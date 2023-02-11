using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
	//��ȭ �����͸� ������ Dictionary ���� ����
	//Dictionary: Key�� ����Ͽ� Value�� ���� ����Ѵ�. (�����迭)
	Dictionary<int, string[]> talkData;
	Dictionary<int, Sprite> portraitData;

	//�ʻ�ȭ ��������Ʈ�� ������ �迭 ����
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
		//��ȭ �ϳ����� ���� ������ ��� �����Ƿ� string[] ���
		talkData.Add(1000, new string[] { "�ȳ�?:1"
										, " �̰��� ó�� �Ա���?:1" });
		talkData.Add(2000, new string[] { "����.:1"
										, " �� ȣ���� ���� �Ƹ�����?:0"
										, "��� �� ȣ������ ������ ����� ������ �ִٰ� ��.:1" });
		talkData.Add(100, new string[] { "����� �������ڴ�." });
		talkData.Add(200, new string[] { "������ ����ߴ� ������ �ִ� å���̴�." });

		//Quest Talk
		talkData.Add(10 + 1000, new string[] { "�ȳ�? ���ο� ���谡����!:0"
											 , "�� ������ ���� ������ �ִٴµ�:1" 
											 , "������ ȣ���ʿ� �絵�� �˷��ٲ���.:0"	});

		talkData.Add(11 + 2000, new string[] { "����.:1"
											 , "�� ȣ���� ������ ������ �°ž�?:0"
											 , "�׷� �� ��Ź�ϳ��� ����ָ� �����ٵ�...:1"
											 , "�� �� ��ó�� ������ ���� �� ã����.:1" });

		talkData.Add(20 + 1000, new string[] { "�絵�� ����?.:1"
											 , "���� �긮�� �ٴϸ� ������!:3"
											 , "���߿� �絵���� �Ѹ��� �ؾ߰ھ�.:3" });

		talkData.Add(20 + 2000, new string[] { "ã���� �� �� ������ ��.:1" });
		talkData.Add(20 + 5000, new string[] { "��ó���� ������ ã�Ҵ�."	});

		talkData.Add(21 + 2000, new string[] { "��, ã���༭ ����.:2"	});


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

	//������ ��ȭ ������ ��ȯ�ϴ� �Լ� �ϳ� ����
	public string GetTalk(int id, int talkIndex)
	{
		if (talkIndex == talkData[id].Length)
			return null;
		else
			return talkData[id][talkIndex];
	}

	//������ �ʻ�ȭ ��������Ʈ�� ��ȯ�� �Լ� ����
	public Sprite GetPortrait(int id, int portraitIndex)
	{
		return portraitData[id + portraitIndex];
	}
}