using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    private GameObject SunObj;


    private void Awake()
    {
        SunObj = GameObject.Find("Sun");
    }
    void Start()
    {
        transform.parent = SunObj.transform;
    }

    void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * 30.0f);
    }
}
