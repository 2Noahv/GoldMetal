using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public GameManager gameManager;
	public AudioClip audioJump;
	public AudioClip audioAttack;
	public AudioClip audioDamaged;
	public AudioClip audioItem;
	public AudioClip audioDie;
	public AudioClip audioFinish;
	public float maxSpeed;
	public float jumpPower;

	Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
	CapsuleCollider2D capsulecollider;
	AudioSource audioSource;

	void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		capsulecollider = GetComponent<CapsuleCollider2D>();
		audioSource = GetComponent<AudioSource>();	
	}	

    //1�ʿ� 60��, �ܹ����� Ű �Է��� ���⼭ ����
	void Update()
	{
		//Jump
		if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping")) {
			rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
			PlaySound("JUMP");
		}

		if (Input.GetButtonUp("Horizontal")) {
            //normalize: ���� ũ�⸦ 1�� ���� ����(��������)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //������ȯ (���� X)
        //Direction Sprite
        if (Input.GetButton("Horizontal")) {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

            //Animation
            if (Mathf.Abs(rigid.velocity.x) < 0.3)
                anim.SetBool("isWalking", false);
			else 
				anim.SetBool("isWalking", true);		
	}

	//1�ʿ� 50ȸ ����
	void FixedUpdate()
    {
        //Move by key control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed) // Right max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
		else if (rigid.velocity.x < maxSpeed*(-1)) // Left max speed
			rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        if(0 > rigid.velocity.y)
        {
			//Landing Platform
			//RayCast: ������Ʈ �˻��� ���� Ray�� ��� ���
			//DrawRay(): ������ �󿡼��� Ray�� �׷��ִ� �Լ�
			Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
			
			//RayCastHit: Ray�� ���� ������Ʈ, ������ �ݶ��̴��� �˻� Ȯ�� ����
			//GetMask(): ���̾� �̸��� �ش��ϴ� �������� �����ϴ� �Լ�
			RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
			if (null != rayHit.collider)
			{
				//distance: Ray�� ����� ���� �Ÿ�
				if (1f > rayHit.distance)
					anim.SetBool("isJumping", false);
			}
		}
	}

	//�������� �浹�� ������ ȣ���ϴ� �Լ� 
	void OnCollisionEnter2D(Collision2D collision)
	{
		if ("Enemy" == collision.gameObject.tag) {
			//Attack
			if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y) {
				OnAttack(collision.transform);
			}
			else //Damaged
			OnDamager(collision.transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if ("Item" == collision.gameObject.tag)
		{
			//Point
			bool isBronze = collision.gameObject.name.Contains("Bronze");
			bool isSilver = collision.gameObject.name.Contains("Silver");
			bool isGold = collision.gameObject.name.Contains("Gold");

			if (isBronze)
				gameManager.stagePoint += 50;
			else if (isSilver)
				gameManager.stagePoint += 100;
			else if (isGold)
				gameManager.stagePoint += 300;


			//Deactive Item
			collision.gameObject.SetActive(false);

			//Sound
			PlaySound("ITEM");
		}
		else if ("Finish" == collision.gameObject.tag) {
			
			//Next Stage
			gameManager.NextStage();
			//Sound
			PlaySound("FINISH");
		}
	}

	void OnAttack(Transform enemy)
	{
		//Point
		gameManager.stagePoint += 100;

		//Player Reaction Force
		rigid.AddForce(Vector2.up *5, ForceMode2D.Impulse);

		//Enemy Die
		EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
		enemyMove.OnDamaged();
		PlaySound("ATTACK");
	}

	void OnDamager(Vector2 targetPos)
	{
		// Helath Down
		gameManager.HealthDown();

		//Change Layer (Immortal ActivE)
		gameObject.layer = 11;

		//View Alpah ������ 0.4f�� ������ �ǹ��Ѵ�.
		spriteRenderer.color = new Color(1, 1, 1, 0.4f);

		//Reaction Force
		int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
		rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

		//Animation
		anim.SetTrigger("doDamaged");

		PlaySound("DAMAGED");

		Invoke("OffDamaged", 2);

		
	}

	void OffDamaged()
	{
		gameObject.layer = 10;
		spriteRenderer.color = new Color(1, 1, 1, 1);
	}

	public void OnDie()
	{
		//Sprite Alpah
		spriteRenderer.color = new Color(1, 1, 1, 0.4f);
		//Sprite Flip Y
		spriteRenderer.flipY = true;
		//Collider Disable
		capsulecollider.enabled = false;
		//Die Effect Jump
		rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
		PlaySound("DIE");
	}

	public void VelocityZero()
	{
		rigid.velocity = Vector2.zero;
	}

	void PlaySound(string action)
	{
		switch (action) {
			case "JUMP":
				audioSource.clip = audioJump;
				break;
			case "ATTACK":
				audioSource.clip = audioAttack;
				break;
			case "DAMAGED":
				audioSource.clip = audioDamaged;
				break;
			case "ITEM":
				audioSource.clip = audioItem;
				break;
			case "DIE":
				audioSource.clip = audioDie;
				break;
			case "FINISH":
				audioSource.clip = audioFinish;
				break;
		}
	}
}
