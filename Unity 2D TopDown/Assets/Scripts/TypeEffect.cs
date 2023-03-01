using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
	//표시할 대화 문자열을 따로 변수로 저장
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

	//대화 문자열을 받는 함수 생성
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

    //애니메이션을 재생을 위한 시작-재생-종료 3개 함수 생성
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
		//대화 문자열과 Text 내용이 일치하면 종료 (재귀 탈출)
		if(msgText.text == targetMsg) {
			EffectEnd();
			return;
		}

		//문자열도 배열처럼 char값에 접근 가능
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
		//종료 함수에서는 대화 마침 아이콘을 활성화 (시작에선 반대) 
		EndCursor.SetActive(true);
	}

}
