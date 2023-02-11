using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
	//대화 데이터를 저장할 Dictionary 변수 생성
	//Dictionary: Key를 사용하여 Value의 값을 취득한다. (연관배열)
	Dictionary<int, string[]> talkData;
	Dictionary<int, Sprite> portraitData;

	//초상화 스프라이트를 저장할 배열 생성
	public Sprite[] portraitArr;

	void Awake()
	{
		talkData = new Dictionary<int, string[]>();
		portraitData = new Dictionary<int, Sprite>();
		GenerateData();
	}

	void GenerateData()
	{
		//Talk Data
		//NPC A:1000, NPC B:2000
		//Box100, DesK:200
		//대화 하나에는 여러 문장이 들어 있으므로 string[] 사용
		talkData.Add(1000, new string[] { "안녕?:1"
										, " 이곳에 처음 왔구나?:1" });
		talkData.Add(2000, new string[] { "여어.:1"
										, " 이 호수는 정말 아름답지?:0"
										, "사실 이 호수에는 무언가의 비밀이 숨겨져 있다고 해.:1" });
		talkData.Add(100, new string[] { "평범한 나무상자다." });
		talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

		//Quest Talk
		talkData.Add(10 + 1000, new string[] { "안녕? 새로운 모험가구나!:0"
											 , "이 마을에 놀라운 전설이 있다는데:1" 
											 , "오른쪽 호수쪽에 루도가 알려줄꺼야.:0"	});

		talkData.Add(11 + 2000, new string[] { "여어.:1"
											 , "이 호수의 전설을 들으러 온거야?:0"
											 , "그럼 내 부탁하나만 들어주면 좋을텐데...:1"
											 , "내 집 근처에 떨어진 동전 좀 찾아줘.:1" });

		talkData.Add(20 + 1000, new string[] { "루도의 동전?.:1"
											 , "돈을 흘리고 다니면 못쓰지!:3"
											 , "나중에 루도에게 한마디 해야겠어.:3" });

		talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘.:1" });
		talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다."	});

		talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워.:2"	});


		//Portrait Data
		//0:Normal, 1:Speak, 2:Happy, 3:Angry
		portraitData.Add(1000 + 0, portraitArr[0]);
		portraitData.Add(1000 + 1, portraitArr[1]);
		portraitData.Add(1000 + 2, portraitArr[2]);
		portraitData.Add(1000 + 3, portraitArr[3]);
		portraitData.Add(2000 + 0, portraitArr[4]);
		portraitData.Add(2000 + 1, portraitArr[5]);
		portraitData.Add(2000 + 2, portraitArr[6]);
		portraitData.Add(2000 + 3, portraitArr[7]);



	}

	//지정된 대화 문장을 반환하는 함수 하나 생성
	public string GetTalk(int id, int talkIndex)
	{
		if (talkIndex == talkData[id].Length)
			return null;
		else
			return talkData[id][talkIndex];
	}

	//지정된 초상화 스프라이트를 반환할 함수 생성
	public Sprite GetPortrait(int id, int portraitIndex)
	{
		return portraitData[id + portraitIndex];
	}
}