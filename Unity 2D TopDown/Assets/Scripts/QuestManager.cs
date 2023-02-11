using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        //int[]에는 해당 퀘스트에 연관된 NPC Id를 입력
        questList.Add(10, new QuestData("마을 사람들과 대화하기."
                                        , new int[] { 1000, 2000 }));
		questList.Add(20, new QuestData("루도의 동전 찾아주기."
							            , new int[] { 5000, 2000 }));

	}

	//NPC Id를 받고 퀘스트번호를 반환하는 함수 생성 
	public int GetQuestTalkIndx(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //Next Talk Target
		if (id == questList[questId].npcId[questActionIndex])
		questActionIndex++;

		//Control Quest Object
		ControlObject();

		//Talk Complete & Next Quest
		//퀘스트 대화순서가 끝에 도달했을 때 퀘스트번호 증가
		if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        //Quest Name
        return questList[questId].questName;
	}

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId) {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
