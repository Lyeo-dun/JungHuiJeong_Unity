using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSceneMove : MonoBehaviour
{
    [Header("Player HP")]
    [SerializeField] private Slider PlayerHPBar;
    private float PlayerHP;

    [Header("Targetting Object")]
    [SerializeField] private Text TargettingText;
    [SerializeField] private GameObject TargettingPanel;
    private GameObject TargettingObject;

    private void Awake()
    {
        PlayerHPBar = GameObject.Find("PlayerHpBar").GetComponent<Slider>();
        TargettingText = GameObject.Find("TargettingText").GetComponent<Text>();
        TargettingPanel = GameObject.Find("TargettingPanel");
    }
    private void Start()
    {
        PlayerHP = 50.0f;
        PlayerHPBar.value = PlayerHP / 100;

        TargettingObject = null;
        TargettingText.text = null;
        TargettingPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            PlayerHP -= 5.0f;

            if(PlayerHP <= 0)
            {
                PlayerHP = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHP += 5.0f;

            if (PlayerHP >= 100)
            {
                PlayerHP = 100;
            }
        }
        PlayerHPBar.value = PlayerHP / 100;

        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.transform.tag == "TargetItem")
                {
                    TargettingPanel.SetActive(true);
                    TargettingObject = hit.transform.gameObject;
                    TargettingText.text = TargettingObject.name;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TargettingPanel.SetActive(false);
            TargettingObject = null;
            TargettingText.text = "";
        }

    }
}
