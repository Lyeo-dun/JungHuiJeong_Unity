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
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Rigid.useGravity = false;

        Collider CollObj = GetComponent<BoxCollider>();
        CollObj.isTrigger = true;

        Rigid.AddForce(transform.forward * 500.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" && other.tag != "WayPoint")
        {
            Debug.Log(other.tag);
            Destroy(this.gameObject);
        }
    }
}
