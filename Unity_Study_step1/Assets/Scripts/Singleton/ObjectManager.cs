using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager Instance = null;

    public static ObjectManager GetInstance
    {
        //��ȯ
        get
        {
            if (Instance == null)
                Instance = new ObjectManager();

            return Instance;
        }
    }//Property

    private ObjectManager() { } //�ܺ� ������ ������� ����(�̱����̱� ������)

    private GameObject EnemyPrefab;
    private List<GameObject> EnemyList = new List<GameObject>();

    private void Awake()
    {
        GameObject ViewObject = new GameObject("EnemyList");
        EnemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
    }
    private void Start()
    {
        for(int i = 0; i < 5; ++i)
        {
            //������Ʈ ����
            GameObject obj = Instantiate(EnemyPrefab);
            obj.transform.parent = GameObject.Find("EnemyList").transform;

            //x = -25 ~ 25
            //z = -25 ~ 25
            //y = -1.0f

            //�����Լ� ��� Random.Range(min, max)
            obj.transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));

            EnemyList.Add(obj);
        }
    }
}
