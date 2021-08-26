using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    private GameObject WayPoint;

    private Vector3 StepPosition;
    private bool isMove;

    private float Speed;

    private Rigidbody Rigid;

    private float idleTime;
    private float BulletShootTime;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();

        if (WayPoint == null)
            WayPoint = new GameObject("WayPoint");

        WayPoint.tag = "WayPoint";

        if (!WayPoint.GetComponent<SphereCollider>())
            WayPoint.AddComponent<SphereCollider>();

        SphereCollider Sphere = WayPoint.GetComponent<SphereCollider>();
        Sphere.radius = 0.2f;
        Sphere.isTrigger = true;
    }

    private void OnEnable()
    {
        Initialized();
    }
    private void Start()
    {
        idleTime = 3.0f;

        Speed = 0.05f;

        BulletShootTime = 1.0f;

        Rigid.useGravity = false;

        Initialized();
    }

    private void FixedUpdate()
    {
        if(isMove)
        {
            transform.position += StepPosition * Speed;
            Debug.DrawLine(WayPoint.transform.position, transform.position);

            BulletShootTime -= Time.deltaTime;
            if (BulletShootTime < 0)
            {
                foreach (GameObject Object in ObjectManager.GetInstance.GetBulletList)
                {
                    if (!Object.activeSelf)
                    {
                        Object.transform.position = transform.position;

                        Object.SetActive(true);

                        BulletShootTime = 3.0f;
                        break;
                    }
                }
            }
        }
        else
        {
            idleTime -= Time.deltaTime;
            if(idleTime < 0)
            {
                WayPoint.transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));
                isMove = true;

                StepPosition = WayPoint.transform.position - transform.position;
                StepPosition.Normalize();//방향만 남긴 벡터
                StepPosition.y = 0;

                //대기시간 세팅
                idleTime = Random.Range(3, 5);
            }
        } 
    }

    private void Initialized()
    {
        //Random.Range(min, max)
        transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));
        WayPoint.transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));

        StepPosition = WayPoint.transform.position - transform.position;
        StepPosition.Normalize();//방향만 남긴 벡터
        StepPosition.y = 0;

        isMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WayPoint")
        {
            isMove = false;
        }
    }   
}
