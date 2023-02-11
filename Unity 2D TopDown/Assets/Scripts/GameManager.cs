using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
	public TalkManager talkManager;
	public QuestManager questManager;
	public GameObject talkPanel;
	public Image protratiImg;
	public Text TalkText;
	public GameObject scanObject;
	public bool isAction;
	public int talkIndex;
	
	public void Action(GameObject scanObj)
	{
		//Get Current Object
		scanObject = scanObj;
		ObjData objData = scanObject.GetComponent<ObjData>();
		Talk(objData.id, objData.isNpc);
		
		//Visible Talk for Action
		talkPanel.SetActive(isAction);
	}

	void Talk(int id, bool isNpc)
	{
		//Set Talk Data
		int questTalkIndex = questManager.GetQuestTalkIndx(id);
		string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);  

		//End Talk
		if(talkData == null)	{
			isAction = false;
			//talkindex는 대화가 끝날 때 0으로 초기화
			talkIndex = 0;
			Debug.Log(questManager.CheckQuest(id));

			return; // void에서 return은 강제 종료 역할
		}

		//Continue Talk
		if (isNpc)	{
			//Split(): 구분자를 통하여 배열로 나눠주는 문자열 함수
			TalkText.text = talkData.Split(':')[0];

			//Parse():문자열을 해당 타입으로 변환해주는 함수
			protratiImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
	

			protratiImg.color = new Color(1, 1, 1, 1);

		}
		else  {
			TalkText.text = talkData;

			protratiImg.color = new Color(1, 1, 1, 0);
		}

		isAction = true;
		talkIndex++;
	}
}
