using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyController : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float Speed;
    
    [Header("Target")]    
    [SerializeField]private GameObject TargetPoint;

    private bool isMove;
    private Vector3 StepPosition;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody Rigid;



    // Start is called before the first frame update
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();

        TargetPoint = GameObject.Find("TargetPoint");
    }//컴포넌트를 들고오는 용도
    
    void Start()
    {
        Rigid.useGravity = false;        
        Speed = 20;

        //StepPosition = Vector3.zero;
        StepPosition = new Vector3(0.0f, 0.0f, 0.0f);

        TargetPoint.transform.position = transform.position;
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

        //Axis 다루기
        //float key = Input.GetAxis("Q");

        //Debug.Log("Q : " + key);


        //마우스 입력을 이용한 이동
        if (Input.GetMouseButton(1))
        {
            //화면에 있는 마우스 위치로 ray를 보내기 위해 정보를 기록
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RayPoint(ray);
        }

        //움직이기
        if (isMove)
            transform.position += StepPosition * Time.deltaTime * Speed; //방향 * 시간 * 속력 = 속도
    
    }

    void RayPoint(Ray _ray)
    {
        //ray가 타겟과 충돌했을 때 반환값을 저장
        RaycastHit hit;

        //(시작 위치와 방향, 충돌한 지점의 정보, 뻗은 거리)
        //ray의 위치와 방향으로부터 Ray Point를 무한하게 발사하고 충돌이 일어나면 hit에 정보를 저장
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity)) //out > 정보를 받아오고 싶을 때 씀
        {
            if (hit.transform.tag == "Ground")
            {
                Debug.DrawLine(_ray.origin, hit.point, Color.red); //ray의 위치로 부터 hit된 위치까지 선을 그림

                //transform.position = new Vector3(hit.point.x, 2.0f, hit.point.z); //확인용, 이 방식으로는 쓰지 말 것!

                Vector3 TmpPos = hit.point;
                TmpPos.y = 2.0f;
                TargetPoint.transform.position = TmpPos;

                isMove = true;

                StepPosition = TargetPoint.transform.position - transform.position;
                StepPosition.Normalize();
                StepPosition.y = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isMove = false;
    }
}
