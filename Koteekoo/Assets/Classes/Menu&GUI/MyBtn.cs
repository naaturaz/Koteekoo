using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBtn : MonoBehaviour {

    Button _btn;
    Vector3 _iniPos;

	// Use this for initialization
	void Start () {
        _btn = GetComponent<Button>();
        _iniPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (name == "Btn_To_Next_Level")
        {
            if (Program.GameScene.EnemyManager.ToNextLevelIsReady())
            {
                Show();
            }
            else
            {
                Hide();

            }
            return;
        }


        var enable = Building.DoWeHavePowerToBuildThis(name) && !Program.GameScene.EnemyManager.ThereIsAnAttackNow();

        if (enable)
        {
            _btn.interactable = true;
        }
        else
        {
            _btn.interactable = false;

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
}
