using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
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
        
        //������Ʈ ����
        _Object.transform.parent = GameObject.Find("DisableList").transform;
        
        //ȿ���� ���� ���� 
        //�������ٴ� ����� Start���� �ѹ��� �ϴ� ���̱� ������ �Ǽ��� ���� üũ�ϴ� ���信�� ���
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
