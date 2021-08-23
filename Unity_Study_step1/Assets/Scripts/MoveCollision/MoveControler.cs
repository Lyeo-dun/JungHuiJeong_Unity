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
        ////���� ������Ʈ�� �������� �̵�(Local)
        //transform.Translate(vPosition * Time.deltaTime * Speed);
        ////Time.deltaTime�� ���� �� ������ ������ ������
        ////Time.deltaTime�� ���� ���� 60�������̶�� �������� �� �ʴ� 60ȸ�� �ݺ��ϱ� ������ ������ ������ �� �ۿ� ����
        ////������ Time.deltaTime(������ ��)�� ���ϸ� �ش� �����Ӹ� ���� ������ õõ�� �����̴� ��ó�� ���δ�
        ////�ڿ������� ������ ǥ�� ����
        ////transform => this.transform(�� ������Ʈ�� Transform)
        ////Transform => Component�� ���� ��

        ////���� ��ǥ�� �������� �̵�(World)
        //transform.Translate(vPosition * Time.deltaTime, Space.World);

        ////��ü�� �� ������ �̵�(Local)
        //transform.Translate(new Vector3(0.0f , 0.0f, Speed * Time.deltaTime));

        ////��ü�� �������� �̵�(World) - ���� ��ǥ�踦 ����
        //transform.Translate(new Vector3(0.0f , 0.0f, Speed * Time.deltaTime), Space.World);

        ////ī�޶� ���� ������
        //transform.Translate(new Vector3(0.0f , 0.0f, Speed) * Time.deltaTime, Camera.main.transform);
    }
}
