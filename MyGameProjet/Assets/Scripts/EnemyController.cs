using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ** �ش� ���۳�Ʈ�� ���� : ���� Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    // ** �̵��� ������
    public GameObject WayPoint;

    // ** �̵� ����
    private bool Move;

    // ** �̵� ����
    private Vector3 Step;

    // ** �̵� �ӵ�
    private float Speed;

    // ** ��������
    private Rigidbody Rigid;

    private void Awake()
    {
        // ** ���� ������Ʈ�� �������� ���۳�Ʈ�� �޾ƿ�
        Rigid = GetComponent<Rigidbody>();

        // ** WayPoint ��� �̸��� ������ ��ǥ������ ����.
        WayPoint = new GameObject("WayPoint");

        // ** WayPoint �� tag �� WayPoint�� ����
        WayPoint.transform.tag = "WayPoint";

        // ** ������ ��ǥ������ �ݶ��̴��� ����.
        WayPoint.AddComponent<SphereCollider>();

        // ** ���ε� �ݶ��̴��� ������ �޾ƿ�
        SphereCollider Sphere = WayPoint.GetComponent<SphereCollider>();

        // ** �ݶ��̴��� ũ�⸦ ����
        Sphere.radius = 0.2f;

        // ** isTrigger = true
        Sphere.isTrigger = true;

    }

    private void Start()
    {
        // ** �̵� �ӵ� ����
        Speed = 0.02f;

        //** �߷� ���� ��Ȱ��ȭ
        Rigid.useGravity = false;

        Initialize();

    }

    private void OnEnable()
    {
        Initialize();
    }


    private void FixedUpdate()
    {
        if(Move == true)
        {
            this.transform.position += Step * Speed;

            Debug.DrawLine(
                this.transform.position,
                WayPoint.transform.position);
        }
    }

    private void Initialize()
    {
        // ** unity ���� ���������� ����
        // ** ���� ������Ʈ�� EnableList�� ���� ��ü��
        this.transform.parent = GameObject.Find("EnableList").transform;
        // ** WayPoint �̵� ��ǥ��ġ :  ���� �Լ� = Random.Range(Min, Max)
        WayPoint.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        //** Ÿ���� �����Ǿ����� �����ϼ� �ֵ��� true�� ����
        Move = true;

        // ** Ÿ���� ������ �ٶ󺸴� ���͸� ����.
        Step = WayPoint.transform.position - this.transform.position;

        // ** ���⸸ �����ְ�
        Step.Normalize();

        // ** ���� ���⿡ Y���� �� ������ ���ֹ���. ���۵� ����.
        Step.y = 0;

        // ** 
        WayPoint.transform.position.Set(
                WayPoint.transform.position.x,
                0.0f, 
                WayPoint.transform.position.z);

        // ** ��ü�� �ش������ �ٶ�.
        this.transform.LookAt(WayPoint.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            Move = false;
            StartCoroutine("EnemyState");
        }
    }

    IEnumerator EnemyState()
    {
        yield return new WaitForSeconds(Random.Range(3, 5));

        Initialize();
    }
}