using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    // ** 따라 다닐 목표 객체
    private GameObject Target;

    //** 얼마만큼의 위치에서 따라다닐 것인지
    private Vector3 Offset;

    [SerializeField] private float ZoomDistance;

    private Camera MainCamera;

    private float ShakeTime;


    private bool isShakeCamera;

    // ** [Const]
    private const float Minimum = 40.0f;
    private const float Maximum = 65.0f;

    private void Awake()
    {
        // ** Target을 설정
        Target = GameObject.Find("Player");

        MainCamera = Camera.main;
    }

    private void Start()
    {
        //transform.parent = Target.transform;

        // ** Target 위치를 설정한다
        Offset = new Vector3(0.0f, 9.0f, -6.0f);
        
        //카메라의 위치를 지정된 장소로 셋팅
        //transform.position = Offset + Target.transform.position;

        transform.rotation = Quaternion.LookRotation((Target.transform.position - transform.position).normalized);

        ZoomDistance = 50.0f;

        // ** 카메라가 흔들릴 시간
        ShakeTime = 0.03f;

        isShakeCamera = false;

    }

    void Update()
    {
        if(!isShakeCamera)
        {

            float MouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");

            // ** 카메라 타겟과의 거리
            ZoomDistance += (MouseScroll * -15);

            if (ZoomDistance < Minimum)
                ZoomDistance = Minimum;
            
            if (ZoomDistance > Maximum)
                ZoomDistance = Maximum;
        }

        // ** 선형 보간 이동 :: 카메라와 타겟간의 거리조정
        MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, ZoomDistance, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.T))
            StartCoroutine("ShakeCamera");

        FollowingCamera();
    }

   void FollowingCamera()
    {
        // ** 카메라가 목표지점까지 이동하는 시간을 셋팅(시작점, 도착 지점, 시작점에서 도착지점까지 갈 때 어느 정도의 간격으로 갈 것인지)
        Vector3 SmoothFollowingPosition = Vector3.Lerp(transform.position, Target.transform.position + Offset, Time.deltaTime * 5.0f);

        // ** 위에서 셋팅된 위치를 반환
        transform.position = SmoothFollowingPosition;
    }
    IEnumerator ShakeCamera()
    {
        isShakeCamera = true;
        // ** 함수가 시작 되자마자 제일 처음으로 카메라 좌표로 받아옴
        Vector3 OldPosition = MainCamera.transform.position;

        while (true)
        {
            yield return new WaitForSeconds(0.005f);

            transform.position = transform.position + 
                Vector3.right * Random.Range(-0.15f, 0.15f) +
                Vector3.up * Random.Range(-0.15f, 0.15f) + 
                Vector3.forward * Random.Range(-0.15f, 0.15f);

            ShakeTime -= Time.deltaTime;
            if (ShakeTime < 0) break;
        }

        // ** 흔들리기 전 기존의 위치로 되돌림
        MainCamera.transform.position = OldPosition;

        ShakeTime = 0.08f;

        isShakeCamera = false;
    }
}
