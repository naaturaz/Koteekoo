﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : General {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, Program.GameScene.Player.transform.position, 0.1f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Program.GameScene.BuildingManager.AddToGen(20);

            Program.GameScene.Player.Power += 20;
            //Destroy(gameObject);
            Program.GameScene.SpawnPool.AddToPool(this);
        }
    }
}
