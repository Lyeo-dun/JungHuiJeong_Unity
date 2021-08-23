using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotate : MonoBehaviour
{
    private float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Speed = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");

        transform.Rotate(Time.deltaTime * fVer * Speed, Time.deltaTime * -fHor * Speed, 0.0f);
    }
}
