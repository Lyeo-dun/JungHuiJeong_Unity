using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SphereMove : MonoBehaviour
{
    private float Speed;
    private bool isMove;

    [SerializeField] private Rigidbody Rigid;
    [SerializeField] private GameObject Target;

    private Vector3 Direction;

    // Start is called before the first frame update
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        Target = GameObject.Find("Target");
    }
    void Start()
    {
        Speed = 20;
        isMove = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isMove = true;
            Direction = Target.transform.position - transform.position;
            Direction.Normalize();
        }

        if(isMove == true)
            transform.Translate(Direction * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tigger Collision!");
    }
}
