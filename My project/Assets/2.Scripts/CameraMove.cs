using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;

	void Awake()
    {
        // FindGameObjectWithTag: �־��� �±׷� ������Ʈ �˻�
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;

	}

    // Update �� �Ŀ� ������Ʈ �ϴ� ����
    // UI������Ʈ, ī�޶� �̵�����
    void LateUpdate()
    {
        transform.position = playerTransform.position + Offset;

	}
}
