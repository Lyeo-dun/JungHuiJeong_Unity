using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Speed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        //Ű �Է¿� ���� �̵�
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");

        transform.Translate(fHor * Speed * Time.deltaTime, 0.0f, fVer * Speed * Time.deltaTime);
    }
}
