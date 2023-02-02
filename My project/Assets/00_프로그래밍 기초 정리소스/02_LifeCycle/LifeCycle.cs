using UnityEngine;

// 초기화 → 물리 → 게임로직 → 해체 

public class LifeCycle : MonoBehaviour
{
	
	// Awake: 게임 오브젝트 생성할 때, 최초 생성
	void Awake()
	{
		Debug.Log("플레이어 데이터가 준비되었습니다.");
	}
	

	// Start: 업데이트 시작 직전, 최초 실행
	// 오브젝트는 변수 transform을 항상 가지고 있다.
	void Start()
	{
		// Translate: 벡터 값을 현재 위치에 더하는 함수 (이동을 의미)
		// 여기서 벡터는 방향과 그에 대한 크기 값을 의미

		// int number = 4; 스칼라 값
		// Vector3 vec = new Vector3(5, 5, 5); // x, y, z 축의 벡터 값
		// transform.Translate(vec);

	}
	
	// 활성화, 켜고 끄고할때 마다 실행된다.
	// OnEnable: 게임 오브젝트가 활성화 되었을 때
	void OnEnable()
	{
		Debug.Log("플레이어가 로그인했습니다.");	
	}

	// FixedUpdate: 물리 연산 업데이트
	// 환경에 상관 없이, 고정된 실행 주기로 CPU를 많이 사용
	void FixedUpdate()
	{
		Debug.Log("이동~");
	}
	
	// Update: 게임 로직 업데이트
	// 환경에 따라 실행 주기가 떨어질 수 있다.
	void Update()
	{
		// Time.deltaTime: 이전 프레임의 완료까지 걸리는 시간
		// 값은 프레임이 적으면 크고, 많으면 작음
		// Translate: 벡터에 곱하기
		// Vector 함수: 시간 매개변수에 곱하기

		Vector3 vec = new Vector3(
			Input.GetAxisRaw("Horizontal") * Time.deltaTime,
			Input.GetAxisRaw("Vertical") * Time.deltaTime, 0);
		transform.Translate(vec);		
		
		// Input: 게임 내 입력을 관리하는 클래스
		// anyKeyDown: 아무 입력을 최초로 받을 때 true		
		if (Input.anyKeyDown)
			Debug.Log("플레이어가 아무 키를 눌렀습니다.");

		//	anyKey:  아무 입력을 받으면 true
		//	if (Input.anyKey)
		//		Debug.Log("플레이어가 아무 키를 누르고 있습니다.");


		// 키보드 입력받기
		if (Input.GetKeyDown(KeyCode.Return))
			Debug.Log("아이템을 구입하였습니다.");

		if (Input.GetKey(KeyCode.LeftArrow))
			Debug.Log("왼쪽으로 이동 중");

		if (Input.GetKeyUp(KeyCode.RightArrow))
			Debug.Log("오른쪽 이동을 멈추었습니다.");

		// 마우스 입력받기 왼쪽 = 0, 오른쪽 = 1
		if (Input.GetMouseButtonDown(0))
			Debug.Log("미사일 발사!");

		if (Input.GetMouseButton(0))
			Debug.Log("미사일 모으는 중...");

		if (Input.GetMouseButtonUp(1))
			Debug.Log("슈퍼 미사일 발사!!");


		// Unity에 저장된 Input Manager 
		// GetButton: Input 버튼 입력을 받으면 true
		// ex
		// if (Input.GetButtonUp("Jump"))
		//	 	Debug.Log("슈퍼 점프!!");

		// GetAxis: 수평, 수직 버튼 입력을 받으면 float
		if (Input.GetButton("Horizontal"))
		{
			Debug.Log($"횡 이동 중...{Input.GetAxis("Horizontal")}");
		}
		
	}
	
	// LateUpdate: 모든 업데이트 끝난 후
	void LateUpdate()
	{
		Debug.Log("경험치 획득.");
	}

	// 모든 Update가 종료되고, Object가 비활성화 되거나 삭제될 때 비활성화 된다. 
	// OnDisable: 게임 오브젝트가 비활성화 되었을 때
	void OnDisable()
	{
		Debug.Log("플레이어가 로그아웃했습니다.");
	}

	// OnDestroy: 게임 오브젝트가 삭제될 때
	void OnDestroy()
	{
		Debug.Log("플레이어 데이터를 해제하였습니다.");
	}	
	
}
