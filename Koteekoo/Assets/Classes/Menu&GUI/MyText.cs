using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyText : MonoBehaviour {

    Text _text;

	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();

        //ManualUpdate();
	}
	
	// Update is called once per frame
	void Update () {


        ManualUpdate();

    }

    void ManualUpdate()
    {
        if (name == "Bullets")
        {
            _text.text = Program.GameScene.Player.BulletsAmt+"";
        }
        else if (name == "Life")
        {
            _text.text = Program.GameScene.Player.Health + "";
        }
        else if (name == "Power")
        {
            _text.text = Program.GameScene.Player.Power + "";
        }
    }
}
