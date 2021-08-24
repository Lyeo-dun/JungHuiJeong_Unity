using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager Instance = null;

    public static ObjectManager GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = new ObjectManager();

            return Instance;
        }
    }

    private ObjectManager() { }

    [SerializeField]private GameObject EnemyPrefab;
    private List<GameObject> EnemyList = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < 5; ++i)
        {
            GameObject obj = Instantiate(EnemyPrefab);
            obj.transform.parent = GameObject.Find("EnemyList").transform;

            //x = -25 ~ 25
            //z = -25 ~ 25
            //y = -1.0f

            //난수함수 사용 Random.Range(min, max)
            obj.transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));

            EnemyList.Add(obj);
        }
    }
}
