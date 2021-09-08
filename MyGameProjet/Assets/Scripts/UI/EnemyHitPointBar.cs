using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHitPointBar : MonoBehaviour
{
    private GameObject HitPointCanvasPrefabs;

    private GameObject HitPointPrefabs;

    [SerializeField] private GameObject HitPoint;
    [SerializeField] private Slider AnchorPoint;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Vector3 CameraPos;

    private float Damage;

    private void Awake()
    {
        HitPointCanvasPrefabs = Resources.Load("Prefab/EnemyHitPointCanvas") as GameObject;
        HitPointPrefabs = Resources.Load("Prefab/HitPointSlider") as GameObject; 
        //�������� ���� �� ������ �ڽ� ��ü�� HitPointCanvas�� �ڽ����� �� ��� �ڽ� �� �������� ���� ���� ������Ʈ�� ��� ã���� ������ ���� �ʾ���
        //�Ŀ� ���ٸ� ���� ���

        HitPoint = Instantiate(HitPointPrefabs);
        AnchorPoint = HitPoint.GetComponent<Slider>();
        CameraPos = Camera.main.transform.position;
    }
    void Start()
    {
        GameObject TmpCanvas = Instantiate(HitPointCanvasPrefabs);
        TmpCanvas.transform.SetParent(transform);
        
        HitPoint.transform.SetParent(TmpCanvas.transform);
        
        AnchorPoint.value = AnchorPoint.maxValue;
        Offset = new Vector3(0.0f, 2.0f, 0.0f);

        Damage = 0.02f;
    }

    void Update()
    {
        CameraPos = transform.position + Offset;
        HitPoint.transform.position = Camera.main.WorldToScreenPoint(CameraPos);

        if (Input.GetKey(KeyCode.O))
        {
            AnchorPoint.value -= Damage;
        }
        if (Input.GetKey(KeyCode.P))
        {
            AnchorPoint.value += Damage;
        } //�÷��̾�� �����ǰ� Ű�� ����
    }
}
