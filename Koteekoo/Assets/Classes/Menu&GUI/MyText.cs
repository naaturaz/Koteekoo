using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

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

        if (name == "Level")
        {
            _text.text = "Level " + Program.GameScene.Level;
        }
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



    int AddOrSubs()
    {
        if (_oldNumber < _newNumber)
        {
            return 1;
        }
        return -1;
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
            //_text.text = Program.GameScene.Player.Power + "";
            SetNewNumber(Program.GameScene.Player.Power);
        }
        else if (name == "Time_Left")
        {
            _text.text = Program.GameScene.TimeLeft();

            if (Program.GameScene.TimeLeft1 <= 1)
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

        Form();

        //SetReachNewNumberIfPossible();

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
            //_text.text = PlayerPrefs.GetInt("Health") + "";
            SetNewNumber(PlayerPrefs.GetInt("Health"));

        }
        else if (name == "GameTime")
        {
            var f = PlayerPrefs.GetFloat("Time");
            _text.text = GameScene.TimeFormat((int)f);
        }
        else if (name == "E_Spent")
        {
            //_text.text = PlayerPrefs.GetInt("Spent") + "";
            SetNewNumber(PlayerPrefs.GetInt("Spent"));
        }
        else if (name == "E_Gen")
        {
            //_text.text = PlayerPrefs.GetInt("Generated") + "";
            SetNewNumber(PlayerPrefs.GetInt("Generated"));
        }
        else if (name == "Kills")
        {
            //_text.text = PlayerPrefs.GetInt("Enemy") + "";
            SetNewNumber(PlayerPrefs.GetInt("Enemy"));
        }
    }

    void SetNewNumber(int newNumb)
    {
        _newNumber = newNumb;
        SetTimeInCourotine();
    }

    /// <summary>
    /// How quick will call the HalfFrame Routine itself 
    /// </summary>
    void SetTimeInCourotine()
    {
        var diff = Math.Abs(_oldNumber - _newNumber);

        if (diff < 151)
        {
            _frameWait = 0.005f;
        }
        else if (diff > 150 && diff < 301)
        {
            _frameWait = 0.002f;// 0.002f;
        }
        else if (diff > 300 && diff < 1000)
        {
            _frameWait = 0.0001f;
        }
        else
        {
            _frameWait = 0.00001f;
        }
    }
}
