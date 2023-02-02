using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    // public�ϸ� �ʱ�ȭ ���ص���
    public float jumpPower;
    public int itemCount;
	public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
	AudioSource audio;

	// ���� ������Ʈ ��, ���� ����
	void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
	}

    // ���� ���� ������Ʈ
	private void Update()
	{
		if (Input.GetButtonDown("Jump") && !isJump) {
            isJump= true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }		

	}

	// �������� ������Ʈ
	void FixedUpdate()
    {
        // GetAxisRaw() 0, 1, -1
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);  // x y z, y�� ������ �� ����Ѵ�.
    }

	private void OnCollisionEnter(Collision collision)
	{
        if ("Floor" == collision.gameObject.tag)
            isJump = false;
	}

	// �ݶ��̴� �浹�� �߻��ϴ� �̺�Ʈ
	// OnTrigger�� Collider�� ��ġ���� Ȯ���� (������ �浹�� �ƴ϶�)
	void OnTriggerEnter(Collider other)
	{
		if ("Item" == other.tag)
		{
			itemCount++;
			audio.Play();
			// SetActive(bool): ������Ʈ Ȱ��ȭ �Լ�
			other.gameObject.SetActive(false);
			manager.GetItem(itemCount);
		}

		else if ("Point" == other.tag)
		{
			// Find �迭 �Լ��� CPU ���ϸ� �ʷ��� �� �����Ƿ� ���ϴ� ���� ����.
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
				// SceneManager: ����� �����ϴ� �⺻ Ŭ����
				// LoadScene(): �־��� ����� �ҷ����� �Լ�
				// �Ű������� ��� ����(int)�� ����
				SceneManager.LoadScene(manager.stage);
			}
			
		}
	}


}
