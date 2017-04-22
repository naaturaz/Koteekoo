using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        Program.GameScene.SoundManager.PlaySound(1);
    }
}
