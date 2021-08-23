using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControler : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float Speed;

    private Vector3 vPosition;//Component

    void Start()
    {
        Speed = 15;
        vPosition = new Vector3(1.0f, 0.0f, 0.0f);
    }
    void Update()
    {        
        ////게임 오브젝트를 기준으로 이동(Local)
        //transform.Translate(vPosition * Time.deltaTime * Speed);
        ////Time.deltaTime이 없을 시 굉장히 빠르게 움직임
        ////Time.deltaTime을 빼면 만약 60프레임이라고 가정했을 시 초당 60회를 반복하기 때문에 빠르게 움직일 수 밖에 없음
        ////하지만 Time.deltaTime(프레임 값)을 곱하면 해당 프레임만 보기 때문에 천천히 움직이는 것처럼 보인다
        ////자연스러운 움직임 표현 가능
        ////transform => this.transform(이 오브젝트의 Transform)
        ////Transform => Component를 만든 것

        ////월드 좌표를 기준으로 이동(World)
        //transform.Translate(vPosition * Time.deltaTime, Space.World);

        ////물체가 앞 쪽으로 이동(Local)
        //transform.Translate(new Vector3(0.0f , 0.0f, Speed * Time.deltaTime));

        ////물체가 앞쪽으로 이동(World) - 절대 좌표계를 따름
        //transform.Translate(new Vector3(0.0f , 0.0f, Speed * Time.deltaTime), Space.World);

        ////카메라 기준 앞으로
        //transform.Translate(new Vector3(0.0f , 0.0f, Speed) * Time.deltaTime, Camera.main.transform);
    }
}
