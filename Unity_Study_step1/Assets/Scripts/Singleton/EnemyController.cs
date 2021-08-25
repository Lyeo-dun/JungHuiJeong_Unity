using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        //난수함수 사용 Random.Range(min, max)
        transform.position = new Vector3(Random.Range(-25, 25), -0.5f, Random.Range(-25, 25));

        transform.parent = GameObject.Find("EnableList").transform;
    }
}
