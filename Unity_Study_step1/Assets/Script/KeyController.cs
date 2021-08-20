using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyController : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float Speed;

    private bool isMove;
    private Vector3 TargetPoint;
    private Vector3 StepPosition;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody Rigid;



    // Start is called before the first frame update
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
    }//컴포넌트를 들고오는 용도
    
    void Start()
    {
        Rigid.useGravity = false;        
        Speed = 15;

        //StepPosition = Vector3.zero; //차이가 뭔지 모르겟습니다..
        StepPosition = new Vector3(0.0f, 0.0f, 0.0f);

        TargetPoint = transform.position;
        isMove = false;
    } //setting 값을 설정할 때 사용하는 용도

    // Update is called once per frame
    void FixedUpdate()
    {
        ////키보드 입력에 대한 이동
        //float fHor = Input.GetAxis("Horizontal");
        //float fVer = Input.GetAxis("Vertical");

        //transform.Translate(fHor * Speed * Time.deltaTime, 0.0f, fVer * Speed * Time.deltaTime);


        /*
         //마우스 입력
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("좌클릭");
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("우클릭");
        }

        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("휠클릭");
        }
        */

        if (Input.GetMouseButton(1))
        {

            isMove = true;
            //화면에 있는 마우스 위치로 ray를 보내기 위해 정보를 기록
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //ray가 타겟과 충돌했을 때 반환값을 저장
            RaycastHit hit;

            //(시작 위치와 방향, 충돌한 지점의 정보, 뻗은 거리)
            //ray의 위치와 방향으로부터 Ray Point를 무한하게 발사하고 충돌이 일어나면 hit에 정보를 저장
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) //out > 정보를 받아오고 싶을 때 씀
            {
                if(hit.transform.tag == "Ground")
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red); //ray의 위치로 부터 hit된 위치까지 선을 그림
                    
                    //transform.position = new Vector3(hit.point.x, 2.0f, hit.point.z); //확인용, 이 방식으로는 쓰지 말 것!

                    TargetPoint = hit.point;
                    TargetPoint.y = 2.0f;
                }

                if (TargetPoint.x - 0.5f < transform.position.x &&
                    TargetPoint.z - 0.5f < transform.position.z &&
                    TargetPoint.z + 0.5f > transform.position.z &&
                    TargetPoint.x + 0.5f > transform.position.x)
                {
                    isMove = false;
                }
                else
                {
                    isMove = true;

                    StepPosition = TargetPoint - transform.position;
                    StepPosition.Normalize();
                    StepPosition.y = 0;//현재 이 방법은 평면이라서 가능한 것
                }
            }

        }

        if (isMove == true)
        {
            //transform.position += StepPosition.normalized;
            transform.LookAt(TargetPoint);
            transform.position += StepPosition;

            if (TargetPoint.x - 0.5f < transform.position.x &&
                TargetPoint.z - 0.5f < transform.position.z &&
                TargetPoint.z + 0.5f > transform.position.z &&
                TargetPoint.x + 0.5f > transform.position.x)
            {
                isMove = false;
            }
           
        }

    }
}
