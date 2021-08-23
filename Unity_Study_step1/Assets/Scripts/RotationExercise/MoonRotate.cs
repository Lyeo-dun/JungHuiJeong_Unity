using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotate : MonoBehaviour
{
    private GameObject EarthObj;


    private void Awake()
    {        
        EarthObj = GameObject.Find("Earth");
    }
    void Start()
    {
        transform.parent = EarthObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
