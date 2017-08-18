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
        StartCoroutine("WaitAlmostASec");


        Form();
    }

    private IEnumerator WaitAlmostASec()
    {
        while (true)
        {
            yield return new WaitForSeconds(.9f);
            ManualUpdate();
        }
    }

    int count;
    // Update is called once per frame
    void Update()
    {
        //count++;
        ////updates every 25 frames. Performance 
        //if (count > 25)
        //{
        //    ManualUpdate();
        //    count = 0;
        //}
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
        else if (name == "Time_Left")
        {
            _text.text = Program.GameScene.TimeLeft();
        }

        else if (name == "Next_Wave_Time_Left")
        {
            if (Program.GameScene.EnemyManager.ToNextLevelIsReady())
            {
                _text.text = "";
                return;
            }
            _text.text = Program.GameScene.EnemyManager.NextWaveAt();
        }
        else if (name == "Next_Wave_Title")
        {
            if (Program.GameScene.EnemyManager.ToNextLevelIsReady())
            {
                _text.text = "";
                return;
            }
        }
        else if (name == "Level")
        {
            _text.text = "Level " + Program.GameScene.Level;
        }

        Form();
    }

    void Form()
    {
        if (name == "Form_Title")
        {
            _text.text = "";
            //var st = PlayerPrefs.GetString("State");

            //if (st == "GameOver")
            //{
            //    _text.text = "Game Over: " + PlayerPrefs.GetString("Reason");
            //}
            //else if (st == "Pass")
            //{
            //    _text.text = "Level " + (PlayerPrefs.GetInt("Current") - 1) + " completed";
            //}
        }
        else if (name == "Game_Over_Title")
        {
            _text.text = "";
            _text.text = "Game Over: " + PlayerPrefs.GetString("Reason");
        }
        else if (name == "Pass_Game_Title")
        {
            _text.text = "";
            _text.text = "Level " + (PlayerPrefs.GetInt("Current") - 1) + " completed";
        }

        else if (name == "Health")
        {
            _text.text = PlayerPrefs.GetInt("Health") + "";
        }
        else if (name == "GameTime")
        {
            var f = PlayerPrefs.GetFloat("Time");

            _text.text = GameScene.TimeFormat((int)f);

        }
        else if (name == "E_Spent")
        {
            _text.text = PlayerPrefs.GetInt("Spent") + "";

        }
        else if (name == "E_Gen")
        {
            _text.text = PlayerPrefs.GetInt("Generated") + "";
        }
        else if (name == "Kills")
        {
            _text.text = PlayerPrefs.GetInt("Enemy") + "";
        }
    }
}
