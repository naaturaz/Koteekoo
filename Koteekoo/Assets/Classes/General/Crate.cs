using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : General {

    public Transform ReachWho;
    public bool DoIReach=true;
    public float ReachSpeed = 0.1f;
    public float DistanceToReachTrue = 0.2f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ReachWho == null)
        {
            ReachWho = Program.GameScene.Player.transform;
        }
        if (DoIReach)
        {
            transform.position = Vector3.MoveTowards(transform.position, ReachWho.position, ReachSpeed);
        }
        CheckIfNearObjective();
    }

    private void CheckIfNearObjective()
    {
        if (UMath.nearEqualByDistance(transform.position, ReachWho.position, DistanceToReachTrue))
        {
            DoActionForThisCrate();
        }
    }

    void DoActionForThisCrate()
    {
        if (name == "Diamond_Drop")
        {
            Program.GameScene.Player.Add1Score();

        }
        else if(name == "Heart_Drop")
        {
            Program.GameScene.Player.Add1Life();
        }
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
        

            Program.GameScene.Player.AddPower(20);

            //Destroy(gameObject);
            Program.GameScene.SpawnPool.AddToPool(this);
        }

    }
}
