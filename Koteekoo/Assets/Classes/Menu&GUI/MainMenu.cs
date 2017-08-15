using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : General {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        Application.LoadLevel("Scn01");
        PlayerPrefs.SetInt("Current", 0);

    }

    public void Tutorial()
    {
        PlayerPrefs.SetString("Tuto", "");
        Application.LoadLevel("Scn01");
        PlayerPrefs.SetInt("Current", 0);
    }

    public void Load(string slot)
    {
        Application.LoadLevel("Scn01");
        var level = PlayerPrefs.GetInt(slot);
        PlayerPrefs.SetInt("Current", level);
        PlayerPrefs.SetString("State", "New");
    }



    public void Exit()
    {
        Application.Quit();
    }

    public void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }



    public void NewLevel()
    {
        Application.LoadLevel("Scn01");
    }

    public void TryAgain()
    {
        Application.LoadLevel("Scn01");
    }


}
