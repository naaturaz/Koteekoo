using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : General {

    GameObject _mainMenu;
    GameObject _passLevelMenu;
    GameObject _gameOverMenu;

    string _oldState;

	// Use this for initialization
	void Start () {
        _mainMenu = GetChildCalled("MainMenu");
        _passLevelMenu = GetChildCalled("PassLevelMenu");
        _gameOverMenu = GetChildCalled("GameOverMenu");

    }

    // Update is called once per frame
    void Update ()
    {
        if (_oldState != PlayerPrefs.GetString("State"))
        {
            UpdateState();
        }
	}

    private void UpdateState()
    {
        _oldState = PlayerPrefs.GetString("State");
        HideAll();

        if (_oldState == "GameOver")
        {
            _gameOverMenu.SetActive(true);

        }
        else if (_oldState == "Pass")
        {
            _passLevelMenu.SetActive(true);

        }
        else
        {
            _mainMenu.SetActive(true);

        }
    }

    void HideAll()
    {
        _mainMenu.SetActive(false);
        _passLevelMenu.SetActive(false);
        _gameOverMenu.SetActive(false);


    }

    public void ShowMainMenu()
    {
        HideAll();
        _mainMenu.SetActive(true);
    }
}
