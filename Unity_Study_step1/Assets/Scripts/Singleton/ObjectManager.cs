using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
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

    //private List<GameObject> EnemyList = new List<GameObject>();

    private List<GameObject> EnableList = new List<GameObject>();
    public List<GameObject> GetEnableList 
    {
        get 
        {
            return EnableList; 
        } 
    }

    private Stack<GameObject> DisableList = new Stack<GameObject>();
    public Stack<GameObject> GetDisableList 
    { 
        get 
        { 
            return DisableList; 
        } 
    }

    public void AddObject(GameObject _Object)
    {
        if(!_Object.GetComponent<EnemyController>())
            _Object.AddComponent<EnemyController>();
        
        //오브젝트 복제
        _Object.transform.parent = GameObject.Find("DisableList").transform;
        
        //효율이 좋지 않음 
        //편리성보다는 현재는 Start에서 한번만 하는 것이기 때문에 실수한 것을 체크하는 개념에서 사용
        _Object.GetComponent<BoxCollider>().isTrigger = true;
        
        //x = -25 ~ 25
        //z = -25 ~ 25
        //y = -1.0f
        
        _Object.transform.position = new Vector3(0.0f, -0.5f, 0.0f);
        
        _Object.gameObject.SetActive(false);
        //EnemyList.Add(obj);
        DisableList.Push(_Object);
    }
}
