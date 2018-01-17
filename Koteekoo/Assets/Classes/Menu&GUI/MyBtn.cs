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
    TutoWindow _tutoWindow;


    // Use this for initialization
    void Start()
    {
        _btn = GetComponent<Button>();
        _iniPos = transform.position;

        _card = FindObjectOfType<Btn_Card>();
        _tutoWindow = FindObjectOfType<TutoWindow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (name == "Btn_To_Next_Wave")
        {
            if (Program.GameScene.EnemyManager.ThereIsAnAttackNow() || !Program.GameScene.EnemyManager.ThereIsMoreWaves()
                || (_tutoWindow && !_tutoWindow.IsDone()))
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
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
