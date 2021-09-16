using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool Horizontal;
    [SerializeField] private float Speed;

    void Start()
    {
        Horizontal = false;
        Speed = 5.0f;
    }

    void Update()
    {

        if (!Horizontal)
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * Speed);

            if (this.transform.position.x < 10.0f)
                Horizontal = true;
        }
        else
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * Speed);


            if (this.transform.position.x > 43.0f)
                Horizontal = false;
        }
    }
}
