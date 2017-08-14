/*
 * Steps to add JoyStick Controller to any game
 * 
 * 1- This class should be always present in each Scene
 * 2- All btns needed to interact with the Controller need to add the class JoyStickBtn
 *      a- In those button if want a Hover effect when selected add a EventTrigger with Select and Unselect events 
 * 3- Add JoyStickPlayerController to the player GO and adjust as necessary
 * 4- Set leftStick axis if needed in Input
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class will be on Scene all time handling every gameObj that will
/// interact with a JoyStick
/// </summary>
public class JoyStickManager : MonoBehaviour
{
    //ordered by Y val
    List<JoyStickBtn> _btnsOrderVertical = new List<JoyStickBtn>();
    ////ordered by X val

    //List<JoyStickBtn> _btnsOrderHorizontal = new List<JoyStickBtn>();

    private float _axisUsedAt;
    private static int _selectedNow = -100;
    private bool _joyStickController;


    //
    public GameObject _buildingBtns;
    public GameObject _inGameMenuBtns;


    private bool _resetBtnsNow;


    public bool JoyStickController
    {
        get
        {
            return _joyStickController;
        }

        set
        {
            _joyStickController = value;
        }
    }

    public bool IsBuilding
    {
        get
        {
            return _isBuilding;
        }

        set
        {
            _isBuilding = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        _buildingBtns = GameObject.Find("BuildingBtns");
        _inGameMenuBtns = GameObject.Find("InGameMenu");

        InitJoy();

        HideInGameBtns();

    }

    void HideInGameBtns()
    {
        //hide them
        if (_buildingBtns != null)
        {
            _buildingBtns.SetActive(false);
            _inGameMenuBtns.SetActive(false);
        }
    }

    private void ResetThis()
    {
        _btnsOrderVertical.Clear();
        _axisUsedAt = 0;
        _selectedNow = -100;
        Debug.Log("Reset This");
    }

    void InitJoy()
    {
        _btnsOrderVertical = FindObjectsOfType<JoyStickBtn>().ToList();
        _btnsOrderVertical = _btnsOrderVertical.OrderBy(a => a.transform.position.x).ThenByDescending(a => a.transform.position.y).ToList();
        Debug.Log("Btns:" + _btnsOrderVertical.Count);

        var joys = Input.GetJoystickNames();
        for (int i = 0; i < joys.Length; i++)
        {
            if (Input.IsJoystickPreconfigured(joys[i]) && joys[i].Contains("Xbox 360"))
            {
                JoyStickController = true;
            }

            Debug.Log(joys[i] + " is preconfig " + Input.IsJoystickPreconfigured(joys[i]));
        }

        //SelectFirstItem();
    }

    // Update is called once per frame
    void Update()
    {
        //will reset if amt of btns is changed at any time, and at least had 1btns on the list 
        //or _resetBtnsNow is true
        CheckIfButtonsChanged();
        InputJoy();

        CheckIfStartPressed();

        if (_isPaused)
        {
            return;
        }

        CheckIfBuildingMode();
    }

    #region Start Btn - Pause

    bool _isPaused;
    private void CheckIfStartPressed()
    {
        if (Input.GetKeyUp(KeyCode.Joystick1Button7) && Application.loadedLevelName != "MainMenu")
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _resetBtnsNow = true;
                _inGameMenuBtns.SetActive(true);
                _buildingBtns.SetActive(false);
                _isBuilding = false;
            }
            else
            {
                _inGameMenuBtns.SetActive(false);
            }
        }
    }



    #endregion

    #region Buildling Mode

    bool _isBuilding;
    /// <summary>
    /// While on game will check ifbuilding mode
    /// </summary>
    private void CheckIfBuildingMode()
    {
        if (Application.loadedLevelName == "MainMenu")
        {
            IsBuilding = true;
            return;
        }

        //can cancel build with B
        var bBtn = IsBuilding && Input.GetKeyUp(KeyCode.Joystick1Button1);
        if ((Input.GetKeyUp(KeyCode.Joystick1Button4) || bBtn) && Application.loadedLevelName != "MainMenu")
        {
            IsBuilding = !IsBuilding;
            Debug.Log("_isBuilding:" + IsBuilding);

            if (IsBuilding)
            {
                AllowBuildNow();
            }
            else
            {
                ForbideBuildNow();
            }
        }
    }

    void AllowBuildNow()
    {
        _buildingBtns.SetActive(true);
        _resetBtnsNow = true;
    }

    void ForbideBuildNow()
    {
        _buildingBtns.SetActive(false);
    }

    void SelectFirstItem()
    {
        _selectedNow = 0;
        _btnsOrderVertical[_selectedNow].Activate();
    }

    #endregion

    /// <summary>
    /// if for some reason buttons just changed we need to reInit
    /// In Koteekoo is used when the GameOver menu is on... bz starts with Main menu then but
    /// quickly changes to GameOver
    /// </summary>
    private void CheckIfButtonsChanged()
    {
        if (_btnsOrderVertical.Count > 0 && !_btnsOrderVertical[0].transform.parent.gameObject.activeSelf ||
            //happens when from Game to main menu 
            _selectedNow > _btnsOrderVertical.Count || _resetBtnsNow)
        {
            _resetBtnsNow = false;
            ResetThis();
            InitJoy();
        }
    }



    float coolDown = 0.15f;

    private void InputJoy()
    {
        //if (Input.GetAxis("Horizontal") > 0 && Time.time > _axisUsedAt + coolDown)
        //{
        //    ToTheRight();
        //}
        //else if (Input.GetAxis("Horizontal") < 0 && Time.time > _axisUsedAt + coolDown)
        //{
        //    ToTheLeft();
        //}

        if (_btnsOrderVertical.Count == 0)
        {
            return;
        }

        if (Input.GetAxis("Vertical") < 0 && Time.time > _axisUsedAt + coolDown)
        {
            ToTheBottom();
        }
        else if (Input.GetAxis("Vertical") > 0 && Time.time > _axisUsedAt + coolDown)
        {
            ToTheTop();
        }
    }

    internal bool ActionButtonNow()
    {
        return JoyStickController && Input.GetKeyUp(KeyCode.JoystickButton0);
    }

    //private void ToTheLeft()
    //{
    //    if (FirstInput())
    //    {
    //        return;
    //    }

    //    ToNextBtnLastStep(-1, _btnsOrderHorizontal);
    //}

    //private void ToTheRight()
    //{
    //    if (FirstInput())
    //    {
    //        return;
    //    }

    //    ToNextBtnLastStep(1, _btnsOrderHorizontal);
    //}

    bool FirstInput()
    {
        if (_selectedNow == -100)
        {
            _selectedNow = 0;
            //select the first button 
            _btnsOrderVertical[_selectedNow].Activate();
            _axisUsedAt = Time.time;
            return true;
        }
        return false;
    }

    internal void ToTheTop()
    {
        if (FirstInput())
        {
            return;
        }

        ToNextBtnLastStep(-1, _btnsOrderVertical);
    }

    internal void ToTheBottom()
    {
        if (FirstInput())
        {
            return;
        }

        ToNextBtnLastStep(1, _btnsOrderVertical);
    }

    void ToNextBtnLastStep(int changeBy, List<JoyStickBtn> list)
    {
        list[_selectedNow].DeActivate();
        ChangeSelectionBy(changeBy);
        list[_selectedNow].Activate();
        _axisUsedAt = Time.time;
    }

    void ChangeSelectionBy(int by)
    {
        if (!IsBuilding && !_isPaused)
        {
            return;
        }

        _selectedNow += by;

        if (_selectedNow < 0)
        {
            _selectedNow = _btnsOrderVertical.Count - 1;
        }
        else if (_selectedNow > _btnsOrderVertical.Count - 1)
        {
            _selectedNow = 0;
        }
    }

    public bool ShouldPauseTime()
    {
        return IsBuilding || _isPaused;
    }
}
