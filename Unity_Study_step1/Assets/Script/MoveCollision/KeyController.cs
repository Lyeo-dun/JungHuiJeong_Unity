using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyController : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float Speed;
    
    [Header("Target")]    
    [SerializeField]private GameObject TargetPoint;

    private bool isMove;
    private Vector3 StepPosition;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody Rigid;



    // Start is called before the first frame update
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();

        TargetPoint = GameObject.Find("TargetPoint");
    }//������Ʈ�� ������� �뵵
    
    void Start()
    {
        Rigid.useGravity = false;        
        Speed = 20;

        //StepPosition = Vector3.zero;
        StepPosition = new Vector3(0.0f, 0.0f, 0.0f);

        TargetPoint.transform.position = transform.position;
        isMove = false;
    } //setting ���� ������ �� ����ϴ� �뵵

    // Update is called once per frame
    void FixedUpdate()
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
            transform.position += StepPosition * Time.deltaTime * Speed; //���� * �ð� * �ӷ� = �ӵ�
    
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
                TmpPos.y = 2.0f;
                TargetPoint.transform.position = TmpPos;

                isMove = true;

                StepPosition = TargetPoint.transform.position - transform.position;
                StepPosition.Normalize();
                StepPosition.y = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isMove = false;
    }
}