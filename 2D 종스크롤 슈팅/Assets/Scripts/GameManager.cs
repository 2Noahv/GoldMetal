using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject[] enemyObjs;
	public Transform[] spawnPoints;

	public float maxSpawnDelay;
	public float curSpawnDeley;

	public GameObject player;
	public Text scoreText;
	public Image[] lifeImage;
	public Image[] boomImage;
	public GameObject gameOverSet;	

	private void Update()
	{
		curSpawnDeley += Time.deltaTime;

		if(curSpawnDeley > maxSpawnDelay) {
			SpawnEnemy();
			maxSpawnDelay = Random.Range(0.5f, 3f);
			curSpawnDeley = 0;
		}

		//#.UI Score Update
		//{0:n0}: 세자리마다 쉼표로 나눠주는 숫자 양식
		Player playerLogic = player.GetComponent<Player>();
		scoreText.text = string.Format("{0:n0}", playerLogic.score); 
	}

	void SpawnEnemy()
	{
		int ranEnemy = Random.Range(0, 3);
		int ranPoint = Random.Range(0, 9);
		GameObject enemy = Instantiate(enemyObjs[ranEnemy]
						 , spawnPoints[ranPoint].position
						 , spawnPoints[ranPoint].rotation);
		Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
		Enemy enemyLogic = enemy.GetComponent<Enemy>();
		enemyLogic.player = player;

		if(ranPoint == 5 || ranPoint == 6) { //#.Right Spawn
			enemy.transform.Rotate(Vector3.back * 90);
			rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
		}
		else if(ranPoint == 7 || ranPoint == 8) { //#.Left Spawn
			enemy.transform.Rotate(Vector3.forward * 90);
			rigid.velocity = new Vector2(enemyLogic.speed, -1);
		}
		else { //#.Front Spawn 
			rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
		}
	}

	public void UpdateLifeIcon(int life)
	{
		//#.UI Life Init Disable
		for (int index = 0; index < 3; index++) {
			lifeImage[index].color = new Color(1, 1, 1, 0);
		}

		//#.UI Life Active
		for (int index = 0; index < life; index++) {
			lifeImage[index].color = new Color(1, 1, 1, 1);
		}
	}

	public void UpdateBoomIcon(int boom)
	{
		//#.UI Boom Init Disable
		for (int index = 0; index < 3; index++)
		{
			boomImage[index].color = new Color(1, 1, 1, 0);
		}

		//#.UI Boom Active
		for (int index = 0; index < boom; index++)
		{
			boomImage[index].color = new Color(1, 1, 1, 1);
		}
	}

	public void RespwanPlayer()
	{
		Invoke("RespwanPlayerExe",2f);
	}

	void RespwanPlayerExe()
	{
		player.transform.position = Vector3.down * 3.5f;
		player.SetActive(true);

		Player playerLogic = player.GetComponent<Player>();
		playerLogic.isHit = false;
	}

	public void GameOver()
	{
		gameOverSet.SetActive(true);
	}

	public void GameRetry()
	{
		SceneManager.LoadScene(0);
	}
}
