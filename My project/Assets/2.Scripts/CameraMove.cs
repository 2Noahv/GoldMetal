using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;

	void Awake()
    {
        // FindGameObjectWithTag: 주어진 태그로 오브젝트 검색
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;

	}

    // Update 된 후에 업데이트 하는 구간
    // UI업데이트, 카메라 이동관련
    void LateUpdate()
    {
        transform.position = playerTransform.position + Offset;

	}
}
