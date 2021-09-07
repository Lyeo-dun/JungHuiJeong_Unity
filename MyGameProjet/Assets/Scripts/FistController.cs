using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** 해당 컴퍼넌트를 삽입 : 현재 Rigidbody
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class FistController : MonoBehaviour
{
    private Rigidbody Rigid;

    private void Awake()
    {
        // ** 현재 오브젝트의 물리엔진 컴퍼넌트를 받아옴
        Rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // ** 중력 엔진을 비활성화
        Rigid.useGravity = false;

        // ** 충돌체를 받아옴
        Collider CollObj = GetComponent<SphereCollider>();

        //** 받아온 충돌체의 Trigger를 켜줌
        CollObj.isTrigger = true;

        // ** 자기 자신의 정면 방향으로 발사
        Rigid.AddForce(this.transform.forward * 500.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {   
            //Enemy 객체 사라지게함
            // ** EnableList에 있던 객체를 DisableList 로 변경
            other.transform.parent = GameObject.Find("DisableList").transform;
            // ** 객체를 DisableList 이동
            ObjectManager.GetInstance.GetDisableList.Push(other.gameObject);
            // ** EnableList 에 있던 객체 참조를 삭제
            ObjectManager.GetInstance.GetEnableList.Remove(other.gameObject);
            // ** 이동이 완료되면 객체를 비활성화
            other.gameObject.SetActive(false);

            // ** 자기 자신을 삭제
            Destroy(this.gameObject);
        }

        if (other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
