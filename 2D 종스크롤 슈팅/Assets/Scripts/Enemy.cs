using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public string enemyName;
	public int enemyScore;
	public float speed;
	public int health;
	public Sprite[] sprites;

	public float maxShotDelay;
	public float curShotDelay;

	public GameObject bulletObjA;
	public GameObject bulletObjB;
	public GameObject itemCoin;
	public GameObject itemPower;
	public GameObject itemBoom;
	public GameObject player;

	SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		Fire();
		Reload();
	}

	void Fire()
	{
		if (curShotDelay < maxShotDelay)
		return;			

		if(enemyName == "S") {
			//Instantiate(): 매개변수 오브젝트를 생성하는 함수
			GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
			Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
			Vector3 dirVec = player.transform.position - transform.position;
			rigid.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
		}
		else if(enemyName == "L") {
			GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right *0.3f, transform.rotation);
			GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

			Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
			Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

			Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
			Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

			rigidR.AddForce(dirVecR.normalized * 7, ForceMode2D.Impulse);
			rigidL.AddForce(dirVecL.normalized * 7, ForceMode2D.Impulse);

		}

		curShotDelay = 0;
	}

	void Reload()
	{
		//딜레이 변수에 Time.deltaTime 계속 더하여 시간 계산
		curShotDelay += Time.deltaTime;
	}


	public void OnHit(int dmg)
	{
		if (health <= 0)
			return;

		health -= dmg;
		spriteRenderer.sprite = sprites[1];
		Invoke("ReturnSprite", 0.1f);

		if (health <= 0) {
			Player playerLogic = player.GetComponent<Player>();
			playerLogic.score += enemyScore;

			//#.Random Ratio
			int ran = Random.Range(0, 10);
			if(ran < 3) { // Not Item 30%
				Debug.Log("Not iTem");
			}
			else if (ran < 6) { //Coin 30%
				Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
			}
			else if (ran < 8) { //Power 20%
				Instantiate(itemPower, transform.position, itemCoin.transform.rotation);
			}
			else if (ran < 10) { //Boom 20%
				Instantiate(itemBoom, transform.position, itemCoin.transform.rotation);
			}

			Destroy(gameObject);
		}		
	}

	void ReturnSprite()
	{
		spriteRenderer.sprite = sprites[0];
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "BorderBullet")
			Destroy(gameObject);
		else if(collision.gameObject.tag == "PlayerBullet") {
			Bullet bullet = collision.gameObject.GetComponent<Bullet>();
			OnHit(bullet.dmg);
		}
	}
}
