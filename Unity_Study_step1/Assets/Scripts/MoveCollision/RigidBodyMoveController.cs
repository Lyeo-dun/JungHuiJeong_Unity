using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]//꼭 필요한 Component를 자동으로 넣어준다
public class RigidBodyMoveController : MonoBehaviour
{
    private Rigidbody Rigid;
    private float Force;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();//자신이 가진 Component를 가져옴
    }

    private void Start()
    {
        Rigid.useGravity = false;

        Force = 2000.0f;
        
        Rigid.AddForce(Vector3.right * Force);
        //Update에서 AddForce시 매 프레임마다 힘이 가해지게 되므로 물체의 움직임이 점점 빨라진다
    }

    // Update is called once per frame
    void Update()
    {

    }
}
