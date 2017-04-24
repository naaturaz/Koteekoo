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
        }//
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
        else if (name == "Form_Title")
        {
            _text.text = "";
            var st = PlayerPrefs.GetString("State");

            if (st == "GameOver")
            {
                _text.text = "Game Over: " + PlayerPrefs.GetString("Reason");
            }
            else if (st == "Pass")
            {
                _text.text = "Level " + (PlayerPrefs.GetInt("Current") - 1) + " completed";
            }
        }




        //    private void SaveLevelStats()
        //{
        //    PlayerPrefs.SetInt("Enemy", EnemyManager.Kills());
        //    PlayerPrefs.SetInt("Generated", BuildingManager.EnergyGenerated());
        //    PlayerPrefs.SetInt("Spent", BuildingManager.EnergySpent());
        //    PlayerPrefs.SetFloat("Time", EnemyManager.TtlTimeOfCurrentGame());
        //    PlayerPrefs.SetInt("Health", Player.Health);

        //}
        else if (name == "Health")
        {
            _text.text = PlayerPrefs.GetInt("Health")+"";
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
