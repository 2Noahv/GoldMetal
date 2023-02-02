using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItmeCan : MonoBehaviour
{
	public float rotateSpeed;

	void Update()
    {
        // Rotate(Vector3): 매개변수 기준으로 회전시키는 함수
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

}
