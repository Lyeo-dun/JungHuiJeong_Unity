using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TmpPlayerHP : MonoBehaviour
{
    private GameObject Player;
    private Slider HPBar;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        HPBar = GetComponent<Slider>();
    }

    private void Update()
    {
        HPBar.value = Player.GetComponent<TmpMove>().GetHP() / 100;
    }
}
