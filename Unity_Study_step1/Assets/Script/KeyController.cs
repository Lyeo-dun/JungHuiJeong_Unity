using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyController : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float Speed;

    private bool isMove;
    private Vector3 TargetPoint;
    private Vector3 StepPosition;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody Rigid;



    // Start is called before the first frame update
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
    }//������Ʈ�� ������ �뵵
    
    void Start()
    {
        Rigid.useGravity = false;        
        Speed = 15;

        //StepPosition = Vector3.zero; //���̰� ���� �𸣰ٽ��ϴ�..
        StepPosition = new Vector3(0.0f, 0.0f, 0.0f);

        TargetPoint = transform.position;
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

        if (Input.GetMouseButton(1))
        {

            isMove = true;
            //ȭ�鿡 �ִ� ���콺 ��ġ�� ray�� ������ ���� ������ ���
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //ray�� Ÿ�ٰ� �浹���� �� ��ȯ���� ����
            RaycastHit hit;

            //(���� ��ġ�� ����, �浹�� ������ ����, ���� �Ÿ�)
            //ray�� ��ġ�� �������κ��� Ray Point�� �����ϰ� �߻��ϰ� �浹�� �Ͼ�� hit�� ������ ����
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) //out > ������ �޾ƿ��� ���� �� ��
            {
                if(hit.transform.tag == "Ground")
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red); //ray�� ��ġ�� ���� hit�� ��ġ���� ���� �׸�
                    
                    //transform.position = new Vector3(hit.point.x, 2.0f, hit.point.z); //Ȯ�ο�, �� ������δ� ���� �� ��!

                    TargetPoint = hit.point;
                    TargetPoint.y = 2.0f;
                }

                if (TargetPoint.x - 0.5f < transform.position.x &&
                    TargetPoint.z - 0.5f < transform.position.z &&
                    TargetPoint.z + 0.5f > transform.position.z &&
                    TargetPoint.x + 0.5f > transform.position.x)
                {
                    isMove = false;
                }
                else
                {
                    isMove = true;

                    StepPosition = TargetPoint - transform.position;
                    StepPosition.Normalize();
                    StepPosition.y = 0;//���� �� ����� ����̶� ������ ��
                }
            }

        }

        if (isMove == true)
        {
            //transform.position += StepPosition.normalized;
            transform.LookAt(TargetPoint);
            transform.position += StepPosition;

            if (TargetPoint.x - 0.5f < transform.position.x &&
                TargetPoint.z - 0.5f < transform.position.z &&
                TargetPoint.z + 0.5f > transform.position.z &&
                TargetPoint.x + 0.5f > transform.position.x)
            {
                isMove = false;
            }
           
        }

    }
}
