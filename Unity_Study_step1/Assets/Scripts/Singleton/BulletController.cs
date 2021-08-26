using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    private Rigidbody Rigid;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        Rigid.AddForce(transform.forward * 500.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        Rigid.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Enemy")
            gameObject.SetActive(false);
    }
}
