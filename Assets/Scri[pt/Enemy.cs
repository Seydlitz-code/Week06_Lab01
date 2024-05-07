using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private int wayPointCount; // �̵� ��� ����
	private Transform[] wayPoints; // �̵� ��� ����
	private int currentIndex = 0; // ���� ��ǥ���� �ε���
	private Movement2D movement2D; // ������Ʈ �̵� ����

	public void Setup(Transform[] wayPoints)
	{
		movement2D = GetComponent<Movement2D>();

		// �� �̵� ��� Waypoint������ �����Ѵ�.
		wayPointCount = wayPoints.Length;
		this.wayPoints = new Transform[wayPointCount];
		this.wayPoints = wayPoints;

		// ���� ��ġ�� ù��° waypoint��ġ�� �����Ѵ�.
		transform.position = wayPoints[currentIndex].position;

		// �� �̵�/ ��ǥ���� ���� �ڷ�ƾ �Լ� ����
		StartCoroutine("OnMove");
	}
	private IEnumerator OnMove()
	{
		// ���� �̵� ������ �����Ѵ�.
		NextMoveTo();

		while (true)
		{
			//�� ������Ʈ�� ȸ����Ų��.
			transform.Rotate(Vector3.forward * 10);

			// ���� ���� ��ġ�� ��ǥ��ġ�� �Ÿ��� 0.2f * movement2D.MoveSpeed���� ���� �� if ���ǹ��� �����Ѵ�.
			if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.2f * movement2D.MoveSpeed)
			{
				// ���� �̵� ������ �����Ѵ�.
				NextMoveTo();
			}

			yield return null;
		}
	}
	private void NextMoveTo()
	{
		// ���� �̵��� waypoint�� �����ִٸ�?
		if (currentIndex < wayPointCount - 1)
		{
			// ��ü�� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� �����Ѵ�.
			transform.position = wayPoints[currentIndex].position;
			// �̵� ���� ���� => ���� ��ǥ ����(waypoint)
			currentIndex++;
			Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
			movement2D.MoveTo(direction);
		}
		//���� ��ġ�� ������ waypoint���?
		else
		{
			// �� ������Ʈ�� �����Ѵ�.
			Destroy(gameObject);
		}
	}
}
