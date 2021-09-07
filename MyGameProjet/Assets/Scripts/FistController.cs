using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** �ش� ���۳�Ʈ�� ���� : ���� Rigidbody
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class FistController : MonoBehaviour
{
    private Rigidbody Rigid;

    private void Awake()
    {
        // ** ���� ������Ʈ�� �������� ���۳�Ʈ�� �޾ƿ�
        Rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // ** �߷� ������ ��Ȱ��ȭ
        Rigid.useGravity = false;

        // ** �浹ü�� �޾ƿ�
        Collider CollObj = GetComponent<SphereCollider>();

        //** �޾ƿ� �浹ü�� Trigger�� ����
        CollObj.isTrigger = true;

        // ** �ڱ� �ڽ��� ���� �������� �߻�
        Rigid.AddForce(this.transform.forward * 500.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {   
            //Enemy ��ü ���������
            // ** EnableList�� �ִ� ��ü�� DisableList �� ����
            other.transform.parent = GameObject.Find("DisableList").transform;
            // ** ��ü�� DisableList �̵�
            ObjectManager.GetInstance.GetDisableList.Push(other.gameObject);
            // ** EnableList �� �ִ� ��ü ������ ����
            ObjectManager.GetInstance.GetEnableList.Remove(other.gameObject);
            // ** �̵��� �Ϸ�Ǹ� ��ü�� ��Ȱ��ȭ
            other.gameObject.SetActive(false);

            // ** �ڱ� �ڽ��� ����
            Destroy(this.gameObject);
        }

        if (other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
