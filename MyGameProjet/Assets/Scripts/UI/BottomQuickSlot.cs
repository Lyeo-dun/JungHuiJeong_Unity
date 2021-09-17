using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BottomQuickSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool ButtonDown = false;
    private bool PopupCheck = false;

    void Start()
    {
        ButtonDown = false;
        PopupCheck = false;
    }

    IEnumerator ButtonUpdate()
    {
        yield return new WaitForSeconds(0.5f);

        if(ButtonDown)
        {
            PopupCheck = true;
            StartCoroutine("PopupUpdate");
        }
    }

    IEnumerator PopupUpdate()
    {
        while(true)
        {
            if (PopupCheck)
            {
                Debug.Log("Popup");
                yield return new WaitForSeconds(0.02f);
            }
            else
                break;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonDown = true;
        StartCoroutine("ButtonUpdate");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!PopupCheck)
            Debug.Log("Skill1");
        
        PopupCheck = false;
        ButtonDown = false;
    }
}
