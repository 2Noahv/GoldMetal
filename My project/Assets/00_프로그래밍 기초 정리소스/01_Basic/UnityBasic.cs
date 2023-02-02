using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class: 클래스 선언에 사용
// MonoBehaviour: 유니티 게임 오브젝트 클래스 상속한 것
public class UnityBasic : MonoBehaviour
{
	// Start is called before the first frame update

    // 전역변수
    int health = 30;
	int level = 5;

	void Start()
    {
        // 콘솔창에 메세지 입력
        Debug.Log("Hello Unity!");

        //1.변수
        float strength = 15.5f;
        string playerName = "나검사";
        bool isFullLevel = false;

        /*
            Debug.Log("용사의 이름은?");
            Debug.Log(playerName);
            Debug.Log("용사의 레벨은?");
            Debug.Log(level);
            Debug.Log("용사의 힘은?");
            Debug.Log(strength);
            Debug.Log("용사는 만렙인가?");
            Debug.Log(isFullLevel);
        */

        //2.그룹형 변수
        string[] monsters = { "슬라임", "사막뱀", "악마" };
        int[] monstersLevel = new int[3];
        monstersLevel[0] = 1;
        monstersLevel[1] = 6;
        monstersLevel[2] = 20;

        Debug.Log("맵에 존재하는 레벨");
        Debug.Log(monstersLevel[0]);
        Debug.Log(monstersLevel[1]);
        Debug.Log(monstersLevel[2]);

        // 제네릭
        List<string> items = new List<string>();
        items.Add("생명물약30");
        items.Add("마나물약30");

        Debug.Log("가지고 있는 아이템");
        Debug.Log(items[0]);
        Debug.Log(items[1]);

        //3.연산자
        int exp = 1500;

        exp = 1500 + 320;
        exp = exp - 10;
        level = exp / 300;
        strength = level * 3.1f;

        Debug.Log("용사의 총 경험치는?");
        Debug.Log(exp);
        Debug.Log("용사의 레벨은?");
        Debug.Log(level);
        Debug.Log("용사의 힘은?");
        Debug.Log(strength);

        int nextExp = 300 - (exp % 300);
        Debug.Log("다음 레벨까지 남은 경험치는?");
        Debug.Log(nextExp);

        string title = "전설의";
        Debug.Log("용사의 이름은?");
        Debug.Log(title + " " + playerName);

        int fullLevel = 99;
        isFullLevel = level == fullLevel;
        Debug.Log("용사는 만렙입니까? " + isFullLevel);

        bool isEndTutorial = level > 10;
        Debug.Log("튜토리얼이 끝난 용사입니까? " + isEndTutorial);

        //int health = 30;
        int mana = 15;
        bool isBadCondition = health <= 50 && mana <= 20;
        Debug.Log("용사의 상태가 나쁩니까? " + isBadCondition);

        string condition = isBadCondition ? "나쁨" : "좋음";

        //4. 키워드, int float string bool new List 등등

        //5. 조건문
        if (condition == "나쁨")
        {
            Debug.Log("플레이어 상태가 나쁘니 아이템을 사용하세요.");
        }
        else
        {
            Debug.Log("플레이어 상태가 좋습니다.");
        }

        if (isBadCondition && items[0] == "생명물약30")
        {
            items.RemoveAt(0);
            health += 30;
            Debug.Log("생명포션30을 사용하였습니다.");
        }
        else if (isBadCondition && items[0] == "마나물약30")
        {
            items.RemoveAt(0);
            mana += 30;
            Debug.Log("마나포션30을 사용하였습니다.");
        }

        switch (monsters[1])
        {
            case "슬라임":
            case "사막뱀":
                Debug.Log("소형 몬스터가 출현!");
                break;
            case "악마":
                Debug.Log("중형 몬스터가 출현!");
                break;
            case "골렘":
                Debug.Log("대형 몬스터가 출현!");
                break;
            default:
                Debug.Log("??? 몬스터가 출현!");
                break;
        }

        //6.반복문
        while (health > 0)
        {
            health--;
            if (health > 0)
                Debug.Log("독 데미지를 입었습니다. " + health);
            else
                Debug.Log("사망하였습니다. ");

            if (health == 10)
            {
                Debug.Log("해독제를 사용합니다. ");
                break;
            }
        }

        for (int count = 0; count < 10; count ++) {
            health++;
            Debug.Log("붕대로 치료 중..." + health);
        }

        // 그룹형변수 길이: .Length(배열)   .Count(리스트)
        for(int index = 0;  index < monsters.Length; index ++) {
            //Debug.Log("이 지역에 있는 몬스터 : " + monsters[index]); 
        }

        // for의 그룹형변수 탐색 특화
        foreach (string monster in monsters) {
            Debug.Log($"이 지역에 있는 몬스터 : {monster}");
        }

        Heal();

        for (int index=0; index < monsters.Length; index ++) {
            Debug.Log($"용사는 {monsters[index]} 에게 {Battle(monstersLevel[index])}"); 
        }

        //8. 클래스
        //인스턴스: 정의된 클래스를 변수 초기화로 실체화
        Player player = new Player();
        player.id = 0;
		player.name = "나법사";
		player.title = "현명한";
        player.strength = 2.4f;
		player.weapon = "나무 지팡이";
        Debug.Log(player.Talk());
        Debug.Log(player.HasWeapon());

        player.LevelUp();
        Debug.Log($"{player.name}의 레벨은 {player.level}입니다.");
        Debug.Log(player.move());
	}          
                
	//7.함수 (메서드)
	void Heal()
	{
        health += 10;
		Debug.Log($"힐을 받았습니다.{health}");
	}
    /*

		// void: 반환 데이터가 없는 함수 타입
		// return: 함수가 값을 반환할 때 사용
		int Heal(int currenthealth)
		{
			currenthealth += 10;
			Debug.Log($"힐을 받았습니다.{currenthealth}");
			return health;
		}
	*/

    string Battle(int monsterLevel)
    {
        string result;
        if (level >= monsterLevel)
            result = "이겼습니다.";
        else
            result = "졌습니다.";

        return result;
    }

}
