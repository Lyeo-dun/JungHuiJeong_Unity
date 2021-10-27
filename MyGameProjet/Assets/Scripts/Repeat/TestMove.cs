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
        // ** Ray가 타겟과 충돌했을때 반환 값을 저장하는 곳.
        RaycastHit hit;

        // ** Physics.Raycast( Ray시작 위치와 방향 , 충돌한 지점의 정보, Mathf.Infinity = 무한한)
        // ** 해석 : ray의 위치와 방향으로부터 RayPoint를 무한하게 발사하고 출동이 일어나면 Hit에 정보를 저장함.

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            // ** 충돌된 객체가 Ground라면
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
