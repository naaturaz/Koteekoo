using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Every btn that will be in interaction with a JoyStick need to have this 
/// </summary>
public class JoyStickBtn : MonoBehaviour
{
    Button _btn;//The unity Button Attached 
    EventTrigger _eventTrigger;
    MyBtn _myBtn;


    // Use this for initialization
    void Start()
    {
        _btn = GetComponent<Button>();
        _eventTrigger = GetComponent<EventTrigger>();
        _myBtn = GetComponent<MyBtn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void DeActivate()
    {
        SelectOrNot(false);
    }

    internal void Activate()
    {
        SelectOrNot(true);
    }

    void SelectOrNot(bool selectNow)
    {
        if (_btn == null)
        {
            return;
        }

        if (selectNow)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_btn.gameObject, null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void Action(Button btn)
    {

    }
}
