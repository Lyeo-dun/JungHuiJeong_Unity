using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointBar : MonoBehaviour
{
    private GameObject HitPoint;
    [SerializeField] private Slider AnchorPoint;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Vector3 CameraPos;

    [SerializeField] private float Damage;

    private void Awake()
    {
        HitPoint = GameObject.Find("HitPointCanvas/HitPointSlider");
        AnchorPoint = HitPoint.GetComponent<Slider>();
        CameraPos = Camera.main.transform.position;
    }
    void Start()
    {
        AnchorPoint.value = AnchorPoint.maxValue;
        Offset = new Vector3(0.0f, 2.5f, 0.0f);

        Damage = 0.02f;
    }
    void Update()
    {
        CameraPos = Offset + transform.position;
        HitPoint.transform.position = Camera.main.WorldToScreenPoint(CameraPos);

        if(Input.GetKey(KeyCode.N))
        {
            AnchorPoint.value -= Damage;
        }
        if (Input.GetKey(KeyCode.M))
        {
            AnchorPoint.value += Damage;
        }
    }
}
