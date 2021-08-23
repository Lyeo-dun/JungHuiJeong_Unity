using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]//�� �ʿ��� Component�� �ڵ����� �־��ش�
public class RigidBodyMoveController : MonoBehaviour
{
    private Rigidbody Rigid;
    private float Force;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();//�ڽ��� ���� Component�� ������
    }

    private void Start()
    {
        Rigid.useGravity = false;

        Force = 2000.0f;
        
        Rigid.AddForce(Vector3.right * Force);
        //Update���� AddForce�� �� �����Ӹ��� ���� �������� �ǹǷ� ��ü�� �������� ���� ��������
    }

    // Update is called once per frame
    void Update()
    {

    }
}
