using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    // ** ���� �ٴ� ��ǥ ��ü
    private GameObject Target;

    //** �󸶸�ŭ�� ��ġ���� ����ٴ� ������
    private Vector3 Offset;

    [SerializeField] private float ZoomDistance;

    private Camera MainCamera;

    private float ShakeTime;


    private bool isShakeCamera;

    // ** [Const]
    private const float Minimum = 40.0f;
    private const float Maximum = 65.0f;

    private void Awake()
    {
        // ** Target�� ����
        Target = GameObject.Find("Player");

        MainCamera = Camera.main;
    }

    private void Start()
    {
        //transform.parent = Target.transform;

        // ** Target ��ġ�� �����Ѵ�
        Offset = new Vector3(0.0f, 9.0f, -6.0f);
        
        //ī�޶��� ��ġ�� ������ ��ҷ� ����
        //transform.position = Offset + Target.transform.position;

        transform.rotation = Quaternion.LookRotation((Target.transform.position - transform.position).normalized);

        ZoomDistance = 50.0f;

        // ** ī�޶� ��鸱 �ð�
        ShakeTime = 0.03f;

        isShakeCamera = false;

    }

    void Update()
    {
        if(!isShakeCamera)
        {

            float MouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");

            // ** ī�޶� Ÿ�ٰ��� �Ÿ�
            ZoomDistance += (MouseScroll * -15);

            if (ZoomDistance < Minimum)
                ZoomDistance = Minimum;
            
            if (ZoomDistance > Maximum)
                ZoomDistance = Maximum;
        }

        // ** ���� ���� �̵� :: ī�޶�� Ÿ�ٰ��� �Ÿ�����
        MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, ZoomDistance, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.T))
            StartCoroutine("ShakeCamera");

        FollowingCamera();
    }

   void FollowingCamera()
    {
        // ** ī�޶� ��ǥ�������� �̵��ϴ� �ð��� ����(������, ���� ����, ���������� ������������ �� �� ��� ������ �������� �� ������)
        Vector3 SmoothFollowingPosition = Vector3.Lerp(transform.position, Target.transform.position + Offset, Time.deltaTime * 5.0f);

        // ** ������ ���õ� ��ġ�� ��ȯ
        transform.position = SmoothFollowingPosition;
    }
    IEnumerator ShakeCamera()
    {
        isShakeCamera = true;
        // ** �Լ��� ���� ���ڸ��� ���� ó������ ī�޶� ��ǥ�� �޾ƿ�
        Vector3 OldPosition = MainCamera.transform.position;

        while (true)
        {
            yield return new WaitForSeconds(0.005f);

            transform.position = transform.position + 
                Vector3.right * Random.Range(-0.15f, 0.15f) +
                Vector3.up * Random.Range(-0.15f, 0.15f) + 
                Vector3.forward * Random.Range(-0.15f, 0.15f);

            ShakeTime -= Time.deltaTime;
            if (ShakeTime < 0) break;
        }

        // ** ��鸮�� �� ������ ��ġ�� �ǵ���
        MainCamera.transform.position = OldPosition;

        ShakeTime = 0.08f;

        isShakeCamera = false;
    }
}
