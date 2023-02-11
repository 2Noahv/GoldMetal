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
			//talkindex�� ��ȭ�� ���� �� 0���� �ʱ�ȭ
			talkIndex = 0;
			Debug.Log(questManager.CheckQuest(id));

			return; // void���� return�� ���� ���� ����
		}

		//Continue Talk
		if (isNpc)	{
			//Split(): �����ڸ� ���Ͽ� �迭�� �����ִ� ���ڿ� �Լ�
			TalkText.text = talkData.Split(':')[0];

			//Parse():���ڿ��� �ش� Ÿ������ ��ȯ���ִ� �Լ�
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
