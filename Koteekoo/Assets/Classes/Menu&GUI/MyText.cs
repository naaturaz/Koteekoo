using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyText : MonoBehaviour
{

    Text _text;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();

        //ManualUpdate();
    }

    // Update is called once per frame
    void Update()
    {


        ManualUpdate();

    }

    void ManualUpdate()
    {
        if (name == "Bullets")
        {
            _text.text = Program.GameScene.Player.Ammo + "";
        }
        else if (name == "Life")
        {
            _text.text = Program.GameScene.Player.Health + "";
        }
        else if (name == "Power")
        {
            _text.text = Program.GameScene.Player.Power + "";
        }
        //
        else if (name == "Time_Left")
        {
            _text.text = Program.GameScene.TimeLeft();
        }
        else if (name == "Level")
        {
            _text.text = "Level " + Program.GameScene.Level;
        }
        else if (name == "Form_Title")
        {
            _text.text = "";
            var st = PlayerPrefs.GetString("State");

            if (st == "GameOver")
            {
                _text.text = "Game Over";
            }
            else if (st == "Pass")
            {
                _text.text = "Level " + (PlayerPrefs.GetInt("Current") - 1) + " completed";

            }
        }
    }
}
