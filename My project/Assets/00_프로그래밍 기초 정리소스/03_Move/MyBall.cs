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
        // GetComponent<T>: �ڽ��� TŸ�� ������Ʈ�� ������
        rigid = GetComponent<Rigidbody>();
    }

	private void Update()
	{
		// [?] ���� ���ϱ�
		// AddForce(Vec):Vec�� ����� ũ��� ���� ��
		// ForceMode: ���� �ִ� ���(����, ���� �ݿ�)
		// Input ������ ������Ʈ��
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
	// �ݶ��̴� �浹�� �߻��ϴ� �̺�Ʈ
	private void OnTriggerEnter(Collider other)
	{

	}
	*/

	// TriggerStay: �ݶ��̴��� ��� �浹�ϰ� ���� �� ȣ��
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

	// RigidBdoy ���� �ڵ�� ���⿡ �ۼ��� ��
	void FixedUpdate()
    {
		// [?] �ӷ� �ٲٱ�
		// velocity: ���� �̵� �ӵ�
		// rigid.velocity = new Vector3(0, 5, 0);

		// [?] ȸ����
		// AddTorque(Vec): Vec ������ ������ ȸ������ ����
		// rigid.AddTorque(Vector3.down);
	}
	
}
