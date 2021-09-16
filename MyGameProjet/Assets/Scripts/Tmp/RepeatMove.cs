using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMove : MonoBehaviour
{
    private float Speed;
    private Vector3 Target;
    void Start()
    {
        Speed = 5.0f;
        Target = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        transform.Translate(Hor * Time.deltaTime * Speed, 0.0f, Ver * Time.deltaTime * Speed);

        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit Hit;

        //    if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
        //    {
        //        Vector3 Dir = Hit.point - transform.position;
        //        Dir.Normalize();

        //        Target = Hit.point;

        //        transform.position = Vector3.Lerp(transform.position, Target, 0.5f);
        //    }
        //}

    }
}
