using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyController : MonoBehaviour
{
    [SerializeField] private float Speed;

    private GameObject TargetPoint;
 
    private bool isMove;

    private Vector3 StepPosition;
    private Rigidbody Rigid;

    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        
        TargetPoint = GameObject.Find("TargetPoint");
        EnemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;

    }//������Ʈ�� ������ �뵵
    
    void Start()
    {
        Rigid.useGravity = false;        
        //StepPosition = Vector3.zero;
        TargetPoint.transform.position = transform.position;

        StepPosition = new Vector3(0.0f, 0.0f, 0.0f);

        Speed = 0.5f;
        isMove = false;

        new GameObject("EnableList");
        new GameObject("DisableList");

        for(int i = 0; i < 5; i++)
            ObjectManager.GetInstance.AddObject(Instantiate(EnemyPrefab));

    } //setting ���� ������ �� ����ϴ� �뵵

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ObjectManager.GetInstance.GetDisableList.Count <= 0)
                for (int i = 0; i < 5; i++)
                    ObjectManager.GetInstance.AddObject(Instantiate(EnemyPrefab));

            GameObject Obj = ObjectManager.GetInstance.GetDisableList.Pop();
            Obj.gameObject.SetActive(true);
            ObjectManager.GetInstance.GetEnableList.Add(Obj);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        ////Ű���� �Է¿� ���� �̵�
        //float fHor = Input.GetAxis("Horizontal");
        //float fVer = Input.GetAxis("Vertical");

        //transform.Translate(fHor * Speed * Time.deltaTime, 0.0f, fVer * Speed * Time.deltaTime);

        /*
         //���콺 �Է�
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("��Ŭ��");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("��Ŭ��");
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("��Ŭ��");
        }
        */

        //Axis �ٷ��
        //float key = Input.GetAxis("Q");

        //Debug.Log("Q : " + key);


        //���콺 �Է��� �̿��� �̵�
        if (Input.GetMouseButton(1))
        {
            //ȭ�鿡 �ִ� ���콺 ��ġ�� ray�� ������ ���� ������ ���
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RayPoint(ray);
        }

        //�����̱�
        if (isMove)
            transform.position += StepPosition * Speed; //(���� * �ð�(FixedUpdate�� deltatime �ʿ� x) * �ӷ�) + ���� ��ġ = ���� ��ġ

    }

    void RayPoint(Ray _ray)
    {
        //ray�� Ÿ�ٰ� �浹���� �� ��ȯ���� ����
        RaycastHit hit;

        //(���� ��ġ�� ����, �浹�� ������ ����, ���� �Ÿ�)
        //ray�� ��ġ�� �������κ��� Ray Point�� �����ϰ� �߻��ϰ� �浹�� �Ͼ�� hit�� ������ ����
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity)) //out > ������ �޾ƿ��� ���� �� ��
        {
            if (hit.transform.tag == "Ground")
            {
                Debug.DrawLine(_ray.origin, hit.point, Color.red); //ray�� ��ġ�� ���� hit�� ��ġ���� ���� �׸�

                //transform.position = new Vector3(hit.point.x, 2.0f, hit.point.z); //Ȯ�ο�, �� ������δ� ���� �� ��!

                Vector3 TmpPos = hit.point;
                TargetPoint.transform.position = TmpPos;

                isMove = true;

                StepPosition = TargetPoint.transform.position - transform.position;
                StepPosition.Normalize();//���⸸ ���� ����
                StepPosition.y = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TargetPoint")
            isMove = false;

        if (other.tag == "Enemy")
        {
            other.gameObject.transform.parent = GameObject.Find("DisableList").transform;

            ObjectManager.GetInstance.GetDisableList.Push(other.gameObject);
            ObjectManager.GetInstance.GetEnableList.Remove(other.gameObject);
            
            other.gameObject.SetActive(false);
        }
    }
}
