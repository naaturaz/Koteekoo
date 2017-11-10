using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickBtnY : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((Input.GetKeyUp(KeyCode.Joystick1Button3)))
        {
            //fire event 
            Program.GameScene.EnemyManager.WaveReadyNow();
        }
	}
}
