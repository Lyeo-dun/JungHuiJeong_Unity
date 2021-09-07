using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //충돌 감지

    //// ** 정면 충돌
    //[SerializeField] private GameObject FrontCollider;

    //// ** 우측 충돌
    //[SerializeField] private GameObject LeftCollider;

    //// ** 좌측 충돌
    //[SerializeField] private GameObject RightCollider;

    //[SerializeField] private float Angle;

    private Ray[] rays = new Ray[3];

    private void Awake()
    {
        //FrontCollider = GameObject.Find("FrontCollider");
        //LeftCollider = GameObject.Find("LeftCollider");
        //RightCollider = GameObject.Find("RightCollider");

        for (int i = 0; i < 3; ++i)
            rays[i] = new Ray();
    }
    void Start()
    {
        //Angle = 30.0f;

        StartCoroutine("GetDirection");
    }

    IEnumerator GetDirection()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < 3; ++i)
                rays[i].origin = transform.position;

            rays[0].direction = transform.forward;

            rays[1].direction = transform.forward - transform.right;

            rays[2].direction = transform.forward - (-transform.right);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ////정면
        //Debug.DrawRay(FrontCollider.transform.position, FrontCollider.transform.forward * 10.0f, Color.blue);

        //Vector3 RightRay = new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(Angle * Mathf.Deg2Rad));
        //Vector3 LeftRay = new Vector3(Mathf.Sin(-Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(-Angle * Mathf.Deg2Rad));

        //Debug.DrawRay(FrontCollider.transform.position, RightRay * 10.0f, Color.blue);
        //Debug.DrawRay(FrontCollider.transform.position, LeftRay * 10.0f, Color.blue);

        //Vector3 Left_RightRay = new Vector3(Mathf.Sin(-Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(-Angle * Mathf.Deg2Rad));
        //Vector3 Left_LeftRay = new Vector3(Mathf.Sin(-Angle * 2 * Mathf.Deg2Rad), 0.0f, Mathf.Cos(-Angle * 2 * Mathf.Deg2Rad));

        //Debug.DrawRay(LeftCollider.transform.position, Left_RightRay * 10.0f, Color.blue);
        //Debug.DrawRay(LeftCollider.transform.position, Left_LeftRay * 10.0f, Color.blue);

        //Vector3 Right_RightRay = new Vector3(Mathf.Sin(Angle * 2 * Mathf.Deg2Rad), 0.0f, Mathf.Cos(Angle * 2 * Mathf.Deg2Rad));
        //Vector3 Right_LeftRay = new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(Angle * Mathf.Deg2Rad));

        //Debug.DrawRay(RightCollider.transform.position, Right_RightRay * 10.0f, Color.blue);
        //Debug.DrawRay(RightCollider.transform.position, Right_LeftRay * 10.0f, Color.blue);


        //RaycastHit Hit;

        /*
        if (Physics.Raycast(FrontCollider.transform.position, FrontCollider.transform.forward, 100.0f))
        {
        }
        */


    }
    private void OnDrawGizmos()//gizmo로만 사용
    {
        for (int i = 0; i < 3; ++i)
            Debug.DrawRay(rays[i].origin, rays[i].direction * 5.0f, Color.blue);
    }
}
