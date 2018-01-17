using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickBtnY : MonoBehaviour {

    TutoWindow _tutoWindow;

	// Use this for initialization
	void Start () {
        _tutoWindow = FindObjectOfType<TutoWindow>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Joystick1Button3) && (_tutoWindow && _tutoWindow.IsDone() || !_tutoWindow))
        {
            //fire event 
            Program.GameScene.EnemyManager.WaveReadyNow();
        }
	}
}
