﻿using UnityEngine;

public class Move : MonoBehaviour
{
	Vector3 target = new Vector3(8, 1.5f, 0);

	void Update()
	{
		//1.MoveTowards: 등속 이동
		// 현재위치, 목표위치, 속도로 구성		
		transform.position =
			Vector3.MoveTowards(transform.position
							, target, 1f);

		//2.SmoothDamp: 부드러운 감속 이동
		// 현재위치, 목표위치, 참조 속도, 속도로 구성
		// ref: 참조 접근-> 실시간으로 바뀌는 값 적용 가능
		Vector3 velo = Vector3.zero;

		transform.position = 
			Vector3.SmoothDamp(transform.position
							, target, ref velo, 0.1f);

		//3.Lerp: 선형 보간, SmoothDamp보다 감속시간이 길다.
		//마지막 매개변수에 비례하여 속도 증가 (최대값 1)
		transform.position = 
			Vector3.Lerp(transform.position
							, target, 0.05f);


		//4.SLerp (구면 선형 보간, 호를 그리며 이동)
		transform.position =
			Vector3.Slerp(transform.position
							, target, 0.01f);
	}

}
