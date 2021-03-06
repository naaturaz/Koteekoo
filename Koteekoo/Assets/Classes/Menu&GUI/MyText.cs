﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MyText : MonoBehaviour
{
    Text _text;

    int _oldNumber;
    int _newNumber;

    AudioSource _sound;

    Vector3 _initialScale;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();
        StartCoroutine("WaitAlmostASec");
        StartCoroutine("HalfFrame");

        _initialScale = transform.localScale;

        Form();

     
    }

    bool _ifFirstHappen;
    float _waitSec = 0.01f;
    private IEnumerator WaitAlmostASec()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitSec);

            if (!_ifFirstHappen)
            {
                _ifFirstHappen = true;
                _waitSec = .9f;
            }

            ManualUpdate();
        }
    }

    float _frameWait = 0.004f;
    private IEnumerator HalfFrame()
    {
        while (true)
        {
            yield return new WaitForSeconds(_frameWait);
            ReachTheNewNumber();
        }
    }

    int count;
    // Update is called once per frame
    void Update()
    {
        count++;
        //updates every 5 frames 
        if (count > 1)
        {
            count = 0;
        }
    }


    #region Reach The Next Number

    private void ReachTheNewNumber()
    {
        if (_oldNumber != _newNumber)
        {
            _oldNumber += AddOrSubs();
            CapOldNumber();
            _text.text = _oldNumber + "";

            ScaleBy(0.3f);
            CapScale(.15f);

            if (_sound != null && _sound.isPlaying)
            {
                return;
            }

            if (_sound == null)
            {
                _sound = Program.GameScene.SoundManager.PlaySound(9, Volume(), false, Pitch(), false);
            }
            else
            {
                _sound = Program.GameScene.SoundManager.PlaySound(_sound, Volume(), Pitch());
            }
        }
        //once the number is reached
        else if (_sound != null)
        {
            _sound.Stop();
            SetToInitialScale();
        }
    }

    private void CapOldNumber()
    {
        if (AddOrSubs() < 0 && _oldNumber < _newNumber)
        {
            _oldNumber = _newNumber;
        }
        if (AddOrSubs() > 0 && _oldNumber > _newNumber)
        {
            _oldNumber = _newNumber;
        }
    }

    int _multiplier = 1;
    int AddOrSubs()
    {
        if (_oldNumber < _newNumber)
        {
            return 1 * _multiplier;
        }
        return -1 * _multiplier;
    }

    float Pitch()
    {
        if (AddOrSubs() > 0)
        {
            return 1.5f;
        }
        return 0.3f;
    }

    float Volume()
    {
        if (AddOrSubs() > 0)
        {
            return .09f;
        }
        return .1f;
    }


    void ScaleBy(float by)
    {
        transform.localScale += new Vector3(by, by, by);
    }

    void CapScale(float at)
    {
        if (transform.localScale.x > _initialScale.x + at)
        {
            transform.localScale = _initialScale + new Vector3(at, at, at);
        }
    }

    private void SetToInitialScale()
    {
        transform.localScale = _initialScale;
    }

    #endregion



    void ManualUpdate()
    {
        if (name == "Level" && Program.GameScene != null)
        {
            _text.text = "Level " + Program.GameScene.Level;


        }
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
            //var powerString = ShortFormat(Program.GameScene.Player.Power);
            SetNewNumber(Program.GameScene.Player.Power);
        }
        else if (name == "Waves_Left")
        {
            _text.text = Program.GameScene.EnemyManager.WavesInThisLevel + " more waves";

            if (Program.GameScene.EnemyManager.WavesInThisLevel == 1)
            {
                _text.text = "1 more wave";
            }
            else if (Program.GameScene.EnemyManager.WavesInThisLevel == 0)
            {
                _text.text = "This last wave!";
            }
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
        else if (name == "Score")
        {
            //_text.text = Program.GameScene.Level;
            SetNewNumber(Program.GameScene.Player.Score);
        }
        else if (name == "Quotes")
        {
            _text.text = Languages.ReturnAQuote(Program.GameScene.Level-2) ;
        }
        Form();

        //SetReachNewNumberIfPossible();

    }



    void Form()
    {
        if (name == "Form_Title")
        {
            _text.text = "";
        }
        else if (name == "Game_Over_Title")
        {
            _text.text = "";
            var reason = PlayerPrefs.GetString("Reason");
            _text.text = "Game Over: " + reason;
        }
        else if (name == "Pass_Game_Title")
        {
            _text.text = "";
            _text.text = "Level " + (PlayerPrefs.GetInt("Current") - 1) + " completed";
        }

        else if (name == "Health")
        {
            SetNewNumber(PlayerPrefs.GetInt("Health"));
        }
        else if (name == "GameTime")
        {
            var f = PlayerPrefs.GetFloat("Time");
            _text.text = GameScene.TimeFormat((int)f);
        }
        else if (name == "E_Spent")
        {
            SetNewNumber(PlayerPrefs.GetInt("Spent"));
        }
        else if (name == "E_Gen")
        {
            SetNewNumber(PlayerPrefs.GetInt("Generated"));
        }
        else if (name == "Kills")
        {
            SetNewNumber(PlayerPrefs.GetInt("Enemy"));
        }
        else if (name == "Final_Score")
        {
            var finalScore = (+
                PlayerPrefs.GetInt("Enemy") +
                PlayerPrefs.GetInt("Generated") + PlayerPrefs.GetInt("Spent")
                + ((PlayerPrefs.GetInt("Health")) * PlayerPrefs.GetInt("Diamonds")));

            SetNewNumber(finalScore);
        }
        else if (name == "Final_Score_Detail")
        {
            _text.text = PlayerPrefs.GetInt("Enemy") + " + " + PlayerPrefs.GetInt("Generated") +
                " + " + +PlayerPrefs.GetInt("Spent") +
                " + (" + (PlayerPrefs.GetInt("Health")) + " * " + PlayerPrefs.GetInt("Diamonds") + ") =";
        }
        else if (name == "Diamonds")
        {
            SetNewNumber(PlayerPrefs.GetInt("Diamonds"));
        }
    }


    void SetNewNumber(int newNumb)
    {
        _newNumber = newNumb;
        SetMultiplier();
    }

    /// <summary>
    /// How quick will call the HalfFrame Routine itself 
    /// </summary>
    void SetMultiplier()
    {
        var diff = Math.Abs(_oldNumber - _newNumber);

        if (diff < 151)
        {
            _multiplier = 1;
        }
        else if (diff > 150 && diff < 301)
        {
            _multiplier = 5;// 0.002f;
        }
        else if (diff > 300 && diff < 1000)
        {
            _multiplier = 10;
        }
        else
        {
            _multiplier = 100;
        }
    }

    private string ShortFormat(float amt)
    {
        if (amt < 10)
        {
            return (amt.ToString("n1"));
        }

        if (amt > 1000000)
        {
            return (int)(amt / 1000000) + "M";
        }
        if (amt > 1000)
        {
            return (int)(amt / 1000) + "K";
        }

        return (int)amt + "";
    }
}
