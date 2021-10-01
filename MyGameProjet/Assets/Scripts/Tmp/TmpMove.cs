using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TmpMove : MonoBehaviour
{
    private Vector3 Target;

    private Vector3 Direction;
    private bool isMove;

    private float Speed;

    [SerializeField] private float PlayerHP;
    private bool ItemCatch;

    [SerializeField] private List<GameObject> WeaponList;
    int CurrentWeapon;

    public GameObject TargetingObject;
    [SerializeField] private Text TargetName;
    [SerializeField] private GameObject TargetPanel;

    private void Awake()
    {
        TargetName = GameObject.Find("TargetName").GetComponent<Text>();
        TargetPanel = GameObject.Find("TargetPanel");
    }
    void Start()
    {
        Target = new Vector3(0.0f, 0.5f, 0.0f);
        Direction = new Vector3(0.0f, 0.5f, 0.0f);
        isMove = false;
        ItemCatch = false;

        Speed = 0.5f;

        CurrentWeapon = 0;
        for(int i = 0; i < WeaponList.Count; i++)
        {
            if (i == CurrentWeapon)
                WeaponList[i].SetActive(true);
            else
                WeaponList[i].SetActive(false);
        }

        PlayerHP = 50.0f;

        TargetName.text = "";

        TargetPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.transform.tag == "Ground")
                {
                    Target = hit.point;
                    Target.y = transform.position.y;

                    Direction = hit.point - transform.position;
                    Direction.Normalize();

                    isMove = true;
                }

                if(hit.transform.tag == "TargetItem")
                {
                    TargetPanel.SetActive(true);

                    TargetingObject = hit.transform.gameObject;
                    TargetName.text = hit.transform.name;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            PlayerHP -= 10;

            if (PlayerHP <= 0)
                PlayerHP = 0;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHP += 10;

            if (PlayerHP >= 100)
                PlayerHP = 100;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TargetingObject = null;
            TargetName.text = "";

            TargetPanel.SetActive(false);
        }

    }

    public float GetHP()
    {
        return PlayerHP;
    }
    public void ChangeWeapon()
    {
        CurrentWeapon++;

        if (CurrentWeapon >= WeaponList.Count)
            CurrentWeapon = 0;

        for (int i = 0; i < WeaponList.Count; i++)
        {
            if (i == CurrentWeapon)
                WeaponList[i].SetActive(true);
            else
                WeaponList[i].SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            ItemCatch = true;
        }
    }
    private void FixedUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, Target, 0.1f);
        
        if(isMove)
        {
            transform.Translate(Direction.x * Speed, 0.0f, Direction.z * Speed);

            if(Vector3.Distance(transform.position, Target) <= 1.0f)
            {
                isMove = false;
            }
        }

        if(ItemCatch)
        {
            PlayerHP += 20;
            ItemCatch = false;
        }
    }
}
