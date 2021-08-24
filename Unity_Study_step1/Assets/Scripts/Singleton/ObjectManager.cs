using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager Instance = null;

    public static ObjectManager GetInstance
    {
        //반환
        get
        {
            if (Instance == null)
                Instance = new ObjectManager();

            return Instance;
        }
    }//Property

    private ObjectManager() { } //외부 생성을 허용하지 않음(싱글톤이기 때문에)

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
            //오브젝트 복제
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
