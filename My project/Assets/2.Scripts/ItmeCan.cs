using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItmeCan : MonoBehaviour
{
	public float rotateSpeed;

	void Update()
    {
        // Rotate(Vector3): �Ű����� �������� ȸ����Ű�� �Լ�
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

}
