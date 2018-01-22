using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MainMenu : General {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        Analytics.CustomEvent("NewGame", new Dictionary<string, object>{{ "NewGame", "BtnClicked" },});

        Application.LoadLevel("Scn01");
        PlayerPrefs.SetInt("Current", 0);

    }

    public void Tutorial()
    {
        Analytics.CustomEvent("Tutorial", new Dictionary<string, object>{{ "Tutorial", "BtnClicked" },});

        PlayerPrefs.SetString("Tuto", "");
        Application.LoadLevel("Scn01");
        PlayerPrefs.SetInt("Current", 0);
    }

    public void Load(string slot)
    {
        Analytics.CustomEvent("Load", new Dictionary<string, object> { { "Load Level", slot }, });

        Application.LoadLevel("Scn01");
        var level = PlayerPrefs.GetInt(slot);
        PlayerPrefs.SetInt("Current", level);
        PlayerPrefs.SetString("State", "New");
    }



    public void Exit()
    {
        Analytics.CustomEvent("Exit", new Dictionary<string, object> { { "Exit", "BtnClicked" }, });

        Application.Quit();
    }

    public void OnApplicationQuit()
    {
        Analytics.CustomEvent("OnQuit", new Dictionary<string, object> {
            { "PlayedTime Sec", Time.time },
            { "PlayedTime Min", Time.time/60 },

        });

        base.OnApplicationQuit();
    }



    public void NewLevel()
    {
        Application.LoadLevel("Scn01");
    }

    public void TryAgain()
    {
        Analytics.CustomEvent("TryAgain", new Dictionary<string, object> { { "TryAgain", "BtnClicked" }, });

        Application.LoadLevel("Scn01");
    }


}
