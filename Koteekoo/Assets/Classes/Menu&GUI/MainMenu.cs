using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        Application.LoadLevel("Scn01");
    }

    public void Load(string slot)
    {
        Application.LoadLevel("Scn01");
        LoadSave.Load(slot);
    }



    public void Exit()
    {
        Application.Quit();
    }
}
