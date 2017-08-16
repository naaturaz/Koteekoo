using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class MyBtn : MonoBehaviour
{

    Button _btn;
    Vector3 _iniPos;
    Btn_Card _card;

    AutoMoveAndRotate _rot;

    // Use this for initialization
    void Start()
    {
        _btn = GetComponent<Button>();
        _iniPos = transform.position;

        _card = FindObjectOfType<Btn_Card>();
        _rot = GetComponent<AutoMoveAndRotate>();

        if (_rot == null)
        {
            return;
        }

        _rot.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (name == "Btn_To_Next_Level")
        {
            if (Program.GameScene.EnemyManager.ToNextLevelIsReady())
            {
                Show();
                Program.GameScene.SoundManager.PlaySound(6);
                _rot.enabled = true;

            }
            else
            {
                Hide();

            }
            return;
        }


        //if (enable)
        //{
        //    _btn.interactable = true;
        //}
        //else
        //{
        //    _btn.interactable = false;

        //}


    }

    private void Hide()
    {
        transform.position += new Vector3(0, 5000, 0);
    }

    private void Show()
    {
        transform.position = _iniPos;
    }

    public void ShowCard()
    {
        _card.Show(name);
    }

    public void HideCard()
    {
        _card.Hide();
    }




}
