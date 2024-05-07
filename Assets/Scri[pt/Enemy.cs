using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private int wayPointCount; // 이동 경로 개수
	private Transform[] wayPoints; // 이동 경로 정보
	private int currentIndex = 0; // 현재 목표지점 인덱스
	private Movement2D movement2D; // 오브젝트 이동 제어

	public void Setup(Transform[] wayPoints)
	{
		movement2D = GetComponent<Movement2D>();

		// 적 이동 경로 Waypoint정보를 설정한다.
		wayPointCount = wayPoints.Length;
		this.wayPoints = new Transform[wayPointCount];
		this.wayPoints = wayPoints;

		// 적의 위치를 첫번째 waypoint위치로 세팅한다.
		transform.position = wayPoints[currentIndex].position;

		// 적 이동/ 목표지점 설정 코루틴 함수 시작
		StartCoroutine("OnMove");
	}
	private IEnumerator OnMove()
	{
		// 다음 이동 방향을 설정한다.
		NextMoveTo();

		while (true)
		{
			//적 오브젝트를 회전시킨다.
			transform.Rotate(Vector3.forward * 10);

			// 적의 현재 위치와 목표위치의 거리가 0.2f * movement2D.MoveSpeed보다 작을 때 if 조건문을 실행한다.
			if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.2f * movement2D.MoveSpeed)
			{
				// 다음 이동 방향을 설정한다.
				NextMoveTo();
			}

			yield return null;
		}
	}
	private void NextMoveTo()
	{
		// 아직 이동할 waypoint가 남아있다면?
		if (currentIndex < wayPointCount - 1)
		{
			// 객체의 위치를 정확하게 목표 위치로 설정한다.
			transform.position = wayPoints[currentIndex].position;
			// 이동 방향 설정 => 다음 목표 지저(waypoint)
			currentIndex++;
			Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
			movement2D.MoveTo(direction);
		}
		//현재 위치가 마지막 waypoint라면?
		else
		{
			// 적 오브젝트를 삭제한다.
			Destroy(gameObject);
		}
	}
}
