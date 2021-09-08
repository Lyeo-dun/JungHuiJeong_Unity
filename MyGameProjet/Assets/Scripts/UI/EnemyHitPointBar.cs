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
        //프리팹을 따로 둔 이유는 자식 객체를 HitPointCanvas의 자식으로 둘 경우 자식 중 가져오고 싶은 게임 오브젝트를 어떻게 찾을지 생각이 나지 않았음
        //후에 배운다면 수정 요망

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
        } //플레이어와 구별되게 키만 변경
    }
}
