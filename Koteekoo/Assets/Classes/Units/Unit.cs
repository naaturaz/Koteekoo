﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Unit : Shooter
{
    protected Transform _enemy;
    AutoMoveAndRotate _rotScript;

    protected Building _building;


    public string Root { get; private set; }

    // Use this for initialization
    protected void Start () {
        base.Start();
        _rotScript = GetComponent<AutoMoveAndRotate>();

    }
	
	// Update is called once per frame
	protected void Update () {
        _enemy = Program.GameScene.EnemyManager.GiveMeClosestEnemy(transform.position);


        if (_building != null && !_building.HasEnergy())
        {
            return;
        }

        if (_enemy != null)
        {
            ShootEnemy();
            _rotScript.enabled = false;
        }
        else
        {
            //stand by
            _rotScript.enabled = true;
        }
    }


    internal static Unit CreateU(string root, Vector3 point, string buildingPath, Transform transform)
    {
        var obj = (Unit)Create(root, point, buildingPath, transform);
        obj.Root = root;
        return obj;
    }

    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (IsDeath())
        {
            Destroy(gameObject, 2);

            if (_building != null)
            {
                Program.GameScene.BuildingManager.RemoveBuilding(_building);
            }

        }
    }

    internal static bool IsAnUnit(string name)
    {
        return name.Contains("Defend");
    }
}
