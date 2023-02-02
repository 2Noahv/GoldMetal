using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBall : MonoBehaviour
{
	// ������Ʈ�� ���� ������ MeshRenderer�� ���ؼ�
	MeshRenderer mesh;
    Material mat;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

	// ���� �������� �浹�� �߻��ϴ� �̺�Ʈ
	// Collision: �浹 ���� Ŭ����
	// CollisionEnter: ������ �浹�� ������ �� ȣ��Ǵ� �Լ�
	private void OnCollisionEnter(Collision collision)
	{
		if ("MyBall" == collision.gameObject.name)
		{
			// color: �⺻ ���� Ŭ����,  Color32: 255 ���� Ŭ����
			mat.color = new Color(0, 0, 0);
		}
	}

	
	// CollisionStay: ������ �浹 ���� ��
	private void OnCollisionStay(Collision collision)
	{

	}
	

	// CollisionExit: ������ �浹�� ������ �� ȣ��Ǵ� �Լ�
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.name == "MyBall")
		{
			mat.color = new Color(1, 1, 1);
		}
	}
	
}
