using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** 해당 컴퍼넌트를 삽입 : 현재 Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    //바라보는 방향
    private Vector3 Direction;

    // ** 이동 속도
    [SerializeField] private float Speed;

    // ** 물리 엔진
    private Rigidbody Rigid;

    // ** Enemy 오브젝트 프리팹을 추가.
    public GameObject EnemyPrefab;

    public GameObject BulletPrefab;

    // ** 총알발사 확인
    private bool BulletCheck;


    void Awake()
    {
        // ** 현재 오브젝트의 물리엔진 컴퍼넌트를 받아옴
        Rigid = GetComponent<Rigidbody>();

        // ** Resources 폴더 안에 있는 리소스를 불러옴.
        // ** Resources.Load("경로") as GameObject;  <= 의 형태 
        EnemyPrefab = Resources.Load("Prefab/EnemyPrefabs/TurtleShellPBR") as GameObject;

        BulletPrefab = Resources.Load("Prefab/Bullets/Bullet") as GameObject;
    }

    void Start()
    {
        // ** 바라보는 방향 초기값
        Direction = Vector3.zero;

        // ** 물리엔진의 중력을 비활성화.
        Rigid.useGravity = false;

        // ** 이동속도
        Speed = 5.0f;

        //** 총알 연속 발사를 제어하기 위함
        BulletCheck = false;

        // ** 하이라키 뷰에 "EnemyList" 이름의 빈 게임 오브젝트를 추가
        //GameObject ViewObject = new GameObject("EnablsList");
        new GameObject("EnableList");
        new GameObject("DisableList");


        for (int i = 0; i < 5; ++i)
        {
            // ** Instantiate = 복제함수
            // ** EnemyPrefab 의 오브젝트를 복제함
            //GameObject Obj = Instantiate(EnemyPrefab);
            //ObjectManager.GetInstance.AddObject(Obj);
            ObjectManager.GetInstance.CreateEnemy(Instantiate(EnemyPrefab));
        }

        // ** Fistall 코루틴 실행.
        StartCoroutine("Fistall");
    }

    private void Update()
    {
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        transform.Translate(Hor * Time.deltaTime * Speed, 0.0f, Ver * Time.deltaTime * Speed);

        
        // ** 현재 상태의 회전값
        //Vector3 CurrentRotation = this.transform.rotation.eulerAngles;

        //CurrentRotation.x += (Input.GetAxisRaw("Mouse Y") * -30);
        //CurrentRotation.y += (Input.GetAxisRaw("Mouse X") * 30);
        //CurrentRotation.z = 0;

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(CurrentRotation), Time.deltaTime * 5.0f);
                

        // ** 스페이스 키 입력을 받았을때 에너미 생성
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ** Stack 에 데이터가 남아있는지 확인하고 없는상태라면 추가한다.
            if (ObjectManager.GetInstance.GetDisableList.Count == 0)
                for (int i = 0; i < 5; ++i)
                    ObjectManager.GetInstance.CreateEnemy(
                        Instantiate(EnemyPrefab));

            // ** GetDisableList 에 있는 객체 하나를 버리고
            GameObject Obj = ObjectManager.GetInstance.GetDisableList.Pop();

            // ** 버려진 객체를 활성화 시켜 사용상태로 변경
            Obj.gameObject.SetActive(true);

            // ** 활성화된 오브젝트를 관리하는 리스트에 포함시킴.
            ObjectManager.GetInstance.GetEnableList.Add(Obj);
        }
        // ** 비활성화 상태에서 활성화 상태로 변경하고, 변경된 오브젝트는 
        // ** 활성화된 오브젝트만 모여있는 리스트에서 사용이 끝날때까지 관리 된다.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RayPoint(ray);

        transform.rotation = Quaternion.LookRotation(Direction);

        if (Input.GetMouseButtonDown(0) && BulletCheck)
        {
            // ** Obj 객체 복사
            GameObject Obj = Instantiate(BulletPrefab);

            // ** 총구의 위치로 이동시킴
            Obj.transform.position = transform.position + (Vector3.up * 0.5f);

            // ** 플레이어가 바라보는 방향으로 세팅
            Obj.transform.rotation = transform.rotation;

            // ** FistController 이름의 스크립트를 복제된 오브젝트에 추가
            Obj.gameObject.AddComponent<FistController>();

            // ** 총알이 한번만 발사 되도록 설정
            BulletCheck = false;

            // ** Fistall 코루틴 실행
            StartCoroutine("Fistall");
        }
    }

    private void FixedUpdate()
    {

    }

    void RayPoint(Ray _ray)
    {
        // ** Ray가 타겟과 충돌했을때 반환 값을 저장하는 곳.
        RaycastHit hit;

        // ** Physics.Raycast( Ray시작 위치와 방향 , 충돌한 지점의 정보, Mathf.Infinity = 무한한)
        // ** 해석 : ray의 위치와 방향으로부터 RayPoint를 무한하게 발사하고 출동이 일어나면 Hit에 정보를 저장함.

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            // ** 충돌된 객체가 Ground라면
            if (hit.transform.tag == "Ground")
            {
                Direction = hit.point - transform.position;
                Direction.Normalize();
            }
        }
    }
    IEnumerator Fistall()
    {
        // ** 해당 시간마다 현재 함수를 재호출
        yield return new WaitForSeconds(0.5f);

        // ** 호출될 때마다 BulletCheck를 true로
        BulletCheck = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ** 충돌된 객체가 Enemy라면
        if (other.tag == "Enemy")
        {
            // ** EnableList에 있던 객체를 DisableList 로 변경
            other.transform.parent = GameObject.Find("DisableList").transform;

            // ** 객체를 DisableList 이동
            ObjectManager.GetInstance.GetDisableList.Push(other.gameObject);

            // ** EnableList 에 있던 객체 참조를 삭제
            ObjectManager.GetInstance.GetEnableList.Remove(other.gameObject);

            // ** 이동이 완료되면 객체를 비활성화
            other.gameObject.SetActive(false);
        }
    }
}


