using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITmp : MonoBehaviour, IPointerDownHandler
{
    private GameObject Player;
    [SerializeField] private int PlayNumber;
    private void Awake()
    {
        Player = GameObject.Find("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Player.GetComponent<RepeatMove>().AniPlaySound(PlayNumber);
    }
    
}
