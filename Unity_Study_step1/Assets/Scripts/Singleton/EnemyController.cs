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

    private bool BulletCheck;

    private GameObject BulletPrefab;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();

        if (WayPoint == null)
            WayPoint = new GameObject("WayPoint");

        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;

        WayPoint.tag = "WayPoint";

        if (!WayPoint.GetComponent<SphereCollider>())
            WayPoint.AddComponent<SphereCollider>();

        SphereCollider Sphere = WayPoint.GetComponent<SphereCollider>();
        Sphere.radius = 0.2f;
        Sphere.isTrigger = true;
    }

    private void OnEnable()
    {
        transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));
        Initialized();
    }
    private void Start()
    {
        Speed = 0.05f;

        Rigid.useGravity = false;

        BulletCheck = false;

        transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));
        Initialized();

        StartCoroutine("BulletShoot");
    }
    private void Update()
    {
        if (BulletCheck)
        {
            GameObject Obj = Instantiate(BulletPrefab);

            Obj.transform.position = transform.position;
            Obj.transform.rotation = transform.rotation;

            Obj.AddComponent<BulletController>();

            BulletCheck = false;
            StartCoroutine("BulletShoot");
        }
    }
    private void FixedUpdate()
    {
        if(isMove)
        {
            transform.position += StepPosition * Speed;
            Debug.DrawLine(WayPoint.transform.position, transform.position);            
        }
    }
    private void Initialized()
    {
        //Random.Range(min, max)       
        WayPoint.transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));
        isMove = true;

        StepPosition = WayPoint.transform.position - transform.position;
        StepPosition.Normalize();//방향만 남긴 벡터
        StepPosition.y = 0;

        WayPoint.transform.position.Set(WayPoint.transform.position.x, transform.position.y, WayPoint.transform.position.z);
        transform.LookAt(WayPoint.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WayPoint")
        {
            isMove = false;
            StartCoroutine("EnemyState");
        }
    }
    IEnumerator BulletShoot()
    {
        yield return new WaitForSeconds(Random.Range(3, 5));
        BulletCheck = true;
    }


    IEnumerator EnemyState()
    {
        yield return new WaitForSeconds(Random.Range(3, 5));
        Initialized();
    }
}
