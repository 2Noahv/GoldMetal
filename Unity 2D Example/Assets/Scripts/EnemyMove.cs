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

		//Invoke(): �־��� �ð��� ���� ��, ������ �Լ��� �����ϴ� �Լ�
		Invoke("Think", 5);
	}

	void FixedUpdate()
	{
		//Move
		rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

		//Platform Check
		//RayCast: ������Ʈ �˻��� ���� Ray�� ��� ���
		//DrawRay(): ������ �󿡼��� Ray�� �׷��ִ� �Լ�
		Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
		Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

		//RayCastHit: Ray�� ���� ������Ʈ, ������ �ݶ��̴��� �˻� Ȯ�� ����
		//GetMask(): ���̾� �̸��� �ش��ϴ� �������� �����ϴ� �Լ�
		RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
		if (null == rayHit.collider)
		{
			Turn();
		}
	}

	// ����Լ�: �ڽ��� ������ ȣ���ϴ� �Լ�
	void Think()
	{
		//Set Next Active
		//Range(): �ּ� ~ �ִ� ������ ���� �� ����(�ִ밪�� ����)
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

		//���� �۵� ���� ��� Invoke�Լ��� ���ߴ� �Լ�
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
