using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Unit {

    NavMeshAgent _agent;

	// Use this for initialization
	void Start () {
        base.Start();
        _agent = gameObject.GetComponent<NavMeshAgent>();
        IsGood = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();

        if (_enemy != null)
        {
            _agent.enabled = true;

            _agent.SetDestination(_enemy.position);
            ShootEnemy();
        }
        else
        {
            //stand by
            _agent.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
