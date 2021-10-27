using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMove : MonoBehaviour
{
    private Animator Anim;
    private bool isRun;

    private Vector3 Direction;
    private NavMeshAgent Nav;

    private Vector3 Target;
    private bool MoveCheck;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Nav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        isRun = false;
        Target = new Vector3(.0f, .0f, .0f);
        MoveCheck = false;
    }

    void Update()
    {
        
        float Hor = Input.GetAxis("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        Anim.SetFloat("Direction", Hor);
        Anim.SetFloat("Speed", Ver);

        if(Input.GetKeyDown(KeyCode.LeftShift))
            isRun = true;

        else if(Input.GetKeyUp(KeyCode.LeftShift))
            isRun = false;

        Anim.SetBool("Run", isRun);

        if(isRun)
            Ver *= 5.0f;

        transform.Translate(0.0f, 0.0f, Ver * Time.deltaTime * 2.0f);

        transform.Rotate(0.0f, Hor * 1.5f, 0.0f);
      


        //if(Input.GetMouseButton(1))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RayPoint(ray);

        //    transform.rotation = Quaternion.LookRotation(Direction);
        //    //MoveCheck = true;
        //}

        //if(Vector3.Distance(Target, transform.position) < 0.1f)
        //{
        //    MoveCheck = false;
        //}

        //Anim.SetBool("Move", MoveCheck);
    }

    void RayPoint(Ray _ray)
    {
        // ** Ray�� Ÿ�ٰ� �浹������ ��ȯ ���� �����ϴ� ��.
        RaycastHit hit;

        // ** Physics.Raycast( Ray���� ��ġ�� ���� , �浹�� ������ ����, Mathf.Infinity = ������)
        // ** �ؼ� : ray�� ��ġ�� �������κ��� RayPoint�� �����ϰ� �߻��ϰ� �⵿�� �Ͼ�� Hit�� ������ ������.

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            // ** �浹�� ��ü�� Ground���
            if (hit.transform.tag == "Ground")
            {
                Direction = hit.point - transform.position;
                Direction.Normalize();

                Target = hit.point;
                Nav.SetDestination(Target);

                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            }
        }
    }
}
