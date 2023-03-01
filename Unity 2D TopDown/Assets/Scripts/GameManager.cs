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
			//talkindex는 대화가 끝날 때 0으로 초기화
			talkIndex = 0;
			questText.text = questManager.CheckQuest(id);

			return; // void에서 return은 강제 종료 역할
		}

		//Continue Talk
		if (isNpc)	{
			//Split(): 구분자를 통하여 배열로 나눠주는 문자열 함수
			talk.SetMsg(talkData.Split(':')[0]);

			//Show Portrait
			//Parse():문자열을 해당 타입으로 변환해주는 함수
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
		//PlayerPrefs: 간단한 데이터 저장 기능을 지원하는 클래스
		PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
		PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
		PlayerPrefs.SetInt("QustId", questManager.questId);
		PlayerPrefs.SetInt("QustActionIndex", questManager.questActionIndex);
		PlayerPrefs.Save();

		menuSet.SetActive(false);	
	}

	public void GameLoad()
	{
		//최초 게임 실행했을 땐 데이터가 없으므로 예외처리 로직 작성
		if (!PlayerPrefs.HasKey("PlayerX"))
			return; 
		
		float x = PlayerPrefs.GetFloat("PlayerX");
		float y = PlayerPrefs.GetFloat("PlayerY");
		int questId = PlayerPrefs.GetInt("QustId");
		int questActionIndex = PlayerPrefs.GetInt("QustActionIndex");

		//불러온 데이터를 게임 오브젝트에 적용
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
