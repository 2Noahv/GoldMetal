using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MyBall : MonoBehaviour
{
	Rigidbody rigid;

    void Start()
    {
        // GetComponent<T>: 자신의 T타입 컴포넌트롤 가져옴
        rigid = GetComponent<Rigidbody>();
    }

	private void Update()
	{
		// [?] 힘을 가하기
		// AddForce(Vec):Vec의 방향과 크기로 힘을 줌
		// ForceMode: 힘을 주는 방식(가속, 무게 반영)
		// Input 관련은 업데이트에
		/*	
		if (Input.GetButtonDown("Jump"))
		{
			rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
		}
		*/

		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		Vector3 vec = new Vector3(h, 0, v);

		rigid.AddForce(vec, ForceMode.Impulse);
	}

	/*
	// 콜라이더 충돌로 발생하는 이벤트
	private void OnTriggerEnter(Collider other)
	{

	}
	*/

	// TriggerStay: 콜라이더가 계속 충돌하고 있을 때 호출
	private void OnTriggerStay(Collider other)
	{
		if ("Cube" == other.name)
			rigid.AddForce(Vector3.up * 4, ForceMode.Impulse);
	}

	/*
	private void OnTriggerExit(Collider other)
	{

	}
	*/

	public void Jump()
	{
		rigid.AddForce(Vector3.up * 50, ForceMode.Impulse);
	}

	// RigidBdoy 관련 코드는 여기에 작성할 것
	void FixedUpdate()
    {
		// [?] 속력 바꾸기
		// velocity: 현재 이동 속도
		// rigid.velocity = new Vector3(0, 5, 0);

		// [?] 회전력
		// AddTorque(Vec): Vec 방향을 축으로 회전력이 생김
		// rigid.AddTorque(Vector3.down);
	}
	
}
