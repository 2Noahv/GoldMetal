using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
	public TalkManager talkManager;
	public QuestManager questManager;
	public Animator talkPanel;
	public Animator portraitAnim;
	public Image protraitImg;
	public Sprite prevPortrait;
	public Text nametext;
	public TypeEffect talk;
	public Text questText;
	public GameObject menuSet;
	public GameObject scanObject;
	public GameObject player;

	public bool isAction;
	public int talkIndex;

	void Start()
	{
		GameLoad();
		questText.text = questManager.CheckQuest();
	}

	void Update()
	{
		//Sub Menu
		if (Input.GetButtonDown("Cancel")) {
			SubMenuActive();
		}
	}

	public void SubMenuActive()
	{
		if (menuSet.activeSelf)
			menuSet.SetActive(false);
		else
			menuSet.SetActive(true);
	}

	public void Action(GameObject scanObj)
	{
		//Get Current Object
		scanObject = scanObj;
		ObjData objData = scanObject.GetComponent<ObjData>();
		Talk(objData.id, objData.isNpc);
		
		//Visible Talk for Action
		talkPanel.SetBool("isShow", isAction);
	}

	void Talk(int id, bool isNpc)
	{
		int questTalkIndex = 0;
		string talkData = "";

		if (talk.isAnim) {
			talk.SetMsg("");
			return;
		}
		else {
			//Set Talk Data
			questTalkIndex = questManager.GetQuestTalkIndx(id);
			talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
		}

		//End Talk
		if (talkData == null)	{
			isAction = false;
			//talkindex�� ��ȭ�� ���� �� 0���� �ʱ�ȭ
			talkIndex = 0;
			questText.text = questManager.CheckQuest(id);

			return; // void���� return�� ���� ���� ����
		}

		//Continue Talk
		if (isNpc)	{
			//Split(): �����ڸ� ���Ͽ� �迭�� �����ִ� ���ڿ� �Լ�
			talk.SetMsg(talkData.Split(':')[0]);

			//Show Portrait
			//Parse():���ڿ��� �ش� Ÿ������ ��ȯ���ִ� �Լ�
			protraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
			protraitImg.color = new Color(1, 1, 1, 1);
			
			//Animation Portrait
			if (prevPortrait != protraitImg.sprite)	{
				portraitAnim.SetTrigger("doEffect");
				prevPortrait = protraitImg.sprite;
			}
		}
		else  {
			talk.SetMsg(talkData);

			protraitImg.color = new Color(1, 1, 1, 0);
		}

		isAction = true;
		talkIndex++;
	}

	public void GameSave()
	{
		//PlayerPrefs: ������ ������ ���� ����� �����ϴ� Ŭ����
		PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
		PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
		PlayerPrefs.SetInt("QustId", questManager.questId);
		PlayerPrefs.SetInt("QustActionIndex", questManager.questActionIndex);
		PlayerPrefs.Save();

		menuSet.SetActive(false);	
	}

	public void GameLoad()
	{
		//���� ���� �������� �� �����Ͱ� �����Ƿ� ����ó�� ���� �ۼ�
		if (!PlayerPrefs.HasKey("PlayerX"))
			return; 
		
		float x = PlayerPrefs.GetFloat("PlayerX");
		float y = PlayerPrefs.GetFloat("PlayerY");
		int questId = PlayerPrefs.GetInt("QustId");
		int questActionIndex = PlayerPrefs.GetInt("QustActionIndex");

		//�ҷ��� �����͸� ���� ������Ʈ�� ����
		player.transform.position = new Vector3(x, y, 0);
		questManager.questId = questId;
		questManager.questActionIndex = questActionIndex;
		questManager.ControlObject();
	}


	public void GameExit()
	{
		Application.Quit();
	}

}
