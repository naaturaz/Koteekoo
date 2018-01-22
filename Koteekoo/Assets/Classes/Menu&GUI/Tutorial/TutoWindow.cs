using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

class TutoWindow : GUIElement
{
    GameObject _btnGotIt;




    /// <summary>
    /// To add tutorial 
    /// add the corresponding entry on Languages and 
    /// then make sure the step is being completed with the call from some
    /// action .... so it needs to be called from somewhere so the step is done
    /// 
    /// if has a 0 as the value then will be completed with a click in 'Got it' btn
    /// </summary>
    Dictionary<string, int> _steps = new Dictionary<string, int>()
    {
        {"Tuto.Objective",0 },
        {"Tuto.Move",1 },
        {"Tuto.Rotate",0 },
        {"Tuto.Shoot",0 },
        {"Tuto.Build",1 },

        { "Tuto.Solar",1 },
        {"Tuto.Place",1 },
        {"Tuto.SetBuild",1 },
        {"Tuto.CancelSolar",1 },
        {"Tuto.Power",0 },

        {"Tuto.Health",0 },
        {"Tuto.Enemy",0 },
        {"Tuto.Turret",0 },
        {"Tuto.Wall",0 },
        {"Tuto.WallArdRocket",1 },

        { "Tuto.Cancel.SmallWall",1 },
        { "Tuto.CreatePath",0 },
        {"Tuto.Waves",0},
        {"Tuto.Jump",0},
        {"Tuto.Restart",0},

        {"Tuto.Move.Block",1 },
        {"Tuto.BigArrow",0 },
        {"Tuto.Tip",0},
        {"Tuto.Tuto",0},

    };

    //will be shown as Tutorial goes on. Some steps have none. And some step have GO
    //in the move step here is where the gameObj rotating circle will be held
    //Also those GO will be assign on inspector
    public GameObject[] StepsGO;

    //which is being shown now 
    int _currentIndex;

    private Text _text;
    private RectTransform _rectTransform;
    private Vector3 _iniPos;

    void HideStepsGO()
    {

        for (int i = 0; i < StepsGO.Length; i++)
        {
            if (StepsGO[i] != null)
            {
                StepsGO[i].SetActive(false);
            }
        }
    }

    void Start()
    {
        _btnGotIt = transform.Find("Got it").gameObject;

        //the helper btn
        _iniPos = transform.position;

        var childText = GetChildCalled("Text");
        _text = childText.GetComponent<Text>();

        _rectTransform = transform.GetComponent<RectTransform>();

        HideStepsGO();

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Tuto")))
        {
            SkipTuto();
        }
        else
        {
            //so it makes sure mosy likely will have power to do everything in the Tuto
            Program.GameScene.Player.Power = 800;
        }
    }


    bool wasShown;
    void Update()
    {
        if (!wasShown && string.IsNullOrEmpty(PlayerPrefs.GetString("Tuto")))
        {
            wasShown = true;
            Show();
        }

        //CheckIfSkipped();
    }

    private void CheckIfSkipped()
    {
        if (Program.GameScene.JoyStickManager.JoyStickController && Input.GetKeyUp(KeyCode.Joystick1Button3))//Y
        {
            SkipTuto();
        }
    }

    public void Hide()
    {
        if (_rectTransform == null)
        {
            return;
        }

        _rectTransform.position = new Vector3(0, -2500);
        _text.text = "";
    }

    internal void Show()
    {
        //when retake from Skipped 
        if (_currentIndex < 0)
        {
            _currentIndex = 0;
        }

        Program.GameScene.SoundManager.PlaySound(1);

        transform.position = _iniPos;

        var which = _steps.ElementAt(_currentIndex).Key;
        _text.text = Languages.ReturnString(which);

        if (_steps.ElementAt(_currentIndex).Value > 0)
        {
            _btnGotIt.SetActive(false);
        }
        else
        {
            _btnGotIt.SetActive(true);
            Program.GameScene.JoyStickManager.SetResetBtnsNowToTrue();
        }

        if (StepsGO[_currentIndex] != null)
        {
            StepsGO[_currentIndex].SetActive(true);
        }
    }

    int totalPotions = 5;
    int totalCoins = 100;
    string weaponID = "Weapon_102";


    public void Next(string step)
    {
        if (_currentIndex == -1 || step != _steps.ElementAt(_currentIndex).Key)
        {
            return;
        }

        Analytics.CustomEvent("tutoStepAchieved", new Dictionary<string, object>
        {
            { "step", step },
        });


        //will hide the StepGO if not hidden already 
        if (StepsGO[_currentIndex] != null)
        {
            StepsGO[_currentIndex].SetActive(false);
        }

        HideArrow();
        _currentIndex++;

        if (_currentIndex >= _steps.Count)
        {
            _currentIndex = -1;
            Hide();

            Program.GameScene.SoundManager.PlaySound(3);

            //when tuto is done the power is 200
            Program.GameScene.Player.Power = 200;

            PlayerPrefs.SetString("Tuto", "Done");
            return;
        }
        //QuestManager.QuestFinished("Tutorial");
        Show();
    }

    public void GotItBtn()
    {
        if (_currentIndex == -1)
        {
            return;
        }

        Next(_steps.ElementAt(_currentIndex).Key);
    }

    /// <summary>
    /// Called from GUI
    /// </summary>
    public void Prev()
    {
        _currentIndex = UMath.Clamper(-1, _currentIndex, 0, _steps.Count - 1);
        Show();
    }

    /// <summary>
    /// Called from GUI
    /// </summary>
    public void SkipTuto()
    {
        HideStepsGO();
        Program.GameScene.SoundManager.PlaySound(1);
        Hide();
        PlayerPrefs.SetString("Tuto", "Skip");
        gameObject.SetActive(false);
        _currentIndex = -1;
    }



    internal bool IsCurrentStep(string accomplishedThisEvent)
    {
        if (_currentIndex == -1)
        {
            return false;
        }

        return _steps.ElementAt(_currentIndex).Key == accomplishedThisEvent;
    }


    internal bool IsDone()
    {
        var state = PlayerPrefs.GetString("Tuto");
        return _currentIndex == -1 && state == "Done";
    }
}







