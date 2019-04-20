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
    GameObject _starBtn;

    // Use this for initialization
    void Start()
    {
        _btn = GetComponent<Button>();
        _eventTrigger = GetComponent<EventTrigger>();
        _myBtn = GetComponent<MyBtn>();

        if(gameObject.transform.Find("StarBtn") != null)
        {
            _starBtn = gameObject.transform.Find("StarBtn").gameObject;
            _starBtn.SetActive(false);
        }
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


            if (_starBtn)
                _starBtn.SetActive(true);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);

            if (_starBtn)
                _starBtn.SetActive(false);
        }
    }

    public void Action(Button btn)
    {

    }

    private void OnDisable()
    {
        if (_starBtn)
            _starBtn.SetActive(false);
    }

    private void OnMouseExit()
    {
        if (_starBtn)
            _starBtn.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_starBtn)
            _starBtn.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_starBtn)
            _starBtn.SetActive(false);
    }
}
