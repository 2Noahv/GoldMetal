using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    // public하면 초기화 안해도됨
    public float jumpPower;
    public int itemCount;
	public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
	AudioSource audio;

	// 게임 오브젝트 때, 최초 실행
	void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
	}

    // 게임 로직 업데이트
	private void Update()
	{
		if (Input.GetButtonDown("Jump") && !isJump) {
            isJump= true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }		

	}

	// 물리연산 업데이트
	void FixedUpdate()
    {
        // GetAxisRaw() 0, 1, -1
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);  // x y z, y는 점프할 때 사용한다.
    }

	private void OnCollisionEnter(Collision collision)
	{
        if ("Floor" == collision.gameObject.tag)
            isJump = false;
	}

	// 콜라이더 충돌로 발생하는 이벤트
	// OnTrigger는 Collider와 겹치는지 확인함 (물리적 충돌이 아니라서)
	void OnTriggerEnter(Collider other)
	{
		if ("Item" == other.tag)
		{
			itemCount++;
			audio.Play();
			// SetActive(bool): 오브젝트 활성화 함수
			other.gameObject.SetActive(false);
			manager.GetItem(itemCount);
		}

		else if ("Point" == other.tag)
		{
			// Find 계열 함수는 CPU 부하를 초래할 수 있으므로 피하는 것이 좋다.
			// GameObject.FindGameObjectWithTag
			if (itemCount == manager.totalItemCount) 
			{
				// Game Clear ! && Next Stage
			    if (2 == manager.stage)
					SceneManager.LoadScene(0);
				else
					SceneManager.LoadScene(manager.stage + 1);
			}
			else {
				// Restart..
				// SceneManager: 장면을 관리하는 기본 클래스
				// LoadScene(): 주어진 장면을 불러오는 함수
				// 매개변수는 장면 순서(int)도 가능
				SceneManager.LoadScene(manager.stage);
			}
			
		}
	}


}
