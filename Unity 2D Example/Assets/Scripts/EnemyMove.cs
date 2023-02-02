using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	Rigidbody2D rigid;
	Animator anim;
	SpriteRenderer spriteRenderer;
	CapsuleCollider2D capsulecollider;

	public int nextMove;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		capsulecollider = GetComponent<CapsuleCollider2D>();

		//Invoke(): 주어진 시간이 지난 뒤, 지정된 함수를 실행하는 함수
		Invoke("Think", 5);
	}

	void FixedUpdate()
	{
		//Move
		rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

		//Platform Check
		//RayCast: 오브젝트 검색을 위해 Ray를 쏘는 방식
		//DrawRay(): 에디터 상에서만 Ray를 그려주는 함수
		Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
		Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

		//RayCastHit: Ray에 닿은 오브젝트, 변수의 콜라이더로 검색 확인 가능
		//GetMask(): 레이어 이름에 해당하는 정수값을 리턴하는 함수
		RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
		if (null == rayHit.collider)
		{
			Turn();
		}
	}

	// 재귀함수: 자신을 스스로 호출하는 함수
	void Think()
	{
		//Set Next Active
		//Range(): 최소 ~ 최대 범위의 랜덤 수 생성(최대값은 제외)
		nextMove = Random.Range(-1, 2);

		//Sprite Animation
		anim.SetInteger("WalkSpeed", nextMove);

		//Flip Sprite
		if (nextMove != 0)
			spriteRenderer.flipX = nextMove == 1;

		//Recursive
		float nextTinkTime = Random.Range(2f, 5f);
		Invoke("Think", nextTinkTime);
	}

	void Turn()
	{
		nextMove *= -1;
		spriteRenderer.flipX = nextMove == 1;

		//현재 작동 중인 모든 Invoke함수를 멈추는 함수
		CancelInvoke();
		Invoke("Think", 5);
	}

	public void OnDamaged()
	{
		//Sprite Alpah
		spriteRenderer.color = new Color(1, 1, 1, 0.4f);
		//Sprite Flip Y
		spriteRenderer.flipY = true;
		//Collider Disable
		capsulecollider.enabled = false;
		//Die Effect Jump
		rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
		//Destroy
		Invoke("DeActive", 5);
	}

	void DeActive()
	{
		gameObject.SetActive(false);
	}
}
