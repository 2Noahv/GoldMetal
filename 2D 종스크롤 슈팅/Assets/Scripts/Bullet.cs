using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int dmg;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "BorderBullet") {
			//Destroy(): 매개변수 오브젝트를 삭제하는 함수
			Destroy(gameObject);
		}
	}
}
