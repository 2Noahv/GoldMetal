using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
	//ǥ���� ��ȭ ���ڿ��� ���� ������ ����
	public int CharPerSecnods;
	public GameObject EndCursor;
	public bool isAnim;

	Text msgText;
	AudioSource audioSource;
	string targetMsg;
	int index;
	float interval;

	private void Awake()
	{
		msgText = GetComponent<Text>();
		audioSource = GetComponent<AudioSource>();
	}

	//��ȭ ���ڿ��� �޴� �Լ� ����
	public void SetMsg(string msg)
    {
		if (isAnim)
		{
			msgText.text = targetMsg;
			CancelInvoke();
			EffectEnd();
		}
		else {
			targetMsg = msg;
			EffectStart();
		}
    }

    //�ִϸ��̼��� ����� ���� ����-���-���� 3�� �Լ� ����
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
		EndCursor.SetActive(false);

		//Start Animation
		interval = 1.0f / CharPerSecnods;
		Debug.Log(interval);

		isAnim = true;
		Invoke("Effecting", interval);
	}

	void Effecting()
    {
		//End Animation
		//��ȭ ���ڿ��� Text ������ ��ġ�ϸ� ���� (��� Ż��)
		if(msgText.text == targetMsg) {
			EffectEnd();
			return;
		}

		//���ڿ��� �迭ó�� char���� ���� ����
		msgText.text += targetMsg[index];
		
		//Sound
		if(targetMsg[index] != ' ' || targetMsg[index] != '.') 
		audioSource.Play();
		
		index++;

		//Recursive
		Invoke("Effecting", interval);
	}


	void EffectEnd()
    {
		isAnim = false;
		//���� �Լ������� ��ȭ ��ħ �������� Ȱ��ȭ (���ۿ��� �ݴ�) 
		EndCursor.SetActive(true);
	}

}
