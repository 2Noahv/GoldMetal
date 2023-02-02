using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//점수와 스테이지를 관리
public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;

    public void NextStage()
    {
        stageIndex++;

        totalPoint += stagePoint;
        stagePoint = 0;
	}

    public void HealthDown()
    {
        if (health > 0)
            health--;
        else {
            //Player Die Effect
            player.OnDie();
			//Result UI

			//Retry Button UI
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if ("Player" == collision.gameObject.tag)
            //Health Down
            HealthDown();

		//Player Reposition
		collision.attachedRigidbody.velocity = Vector2.zero;
        collision.transform.position = new Vector3(2.5f, 2.5f, -1);
	}



}
