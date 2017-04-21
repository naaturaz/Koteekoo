using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveSlot(string numb)
    {
        PlayerPrefs.SetInt(numb, Program.GameScene.Level);
        //LoadSave.Save(numb);
    }

    public void GoToMainMenu()
    {
        PlayerPrefs.SetInt("Current", 0);//Clear current game 
        Application.LoadLevel("MainMenu");
        PlayerPrefs.SetString("State", "New");

    }

    public void ToNextLevelReady()
    {


        Program.GameScene.PassLevel();

    }

    public void Restart()
    {
        Application.LoadLevel("Scn01");
    }
}
