using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Unit : Shooter
{
    protected Transform _enemy;
    float _enemyDist = 100;

    AutoMoveAndRotate _rotScript;

    protected Building _building;


    public string Root { get; private set; }

    // Use this for initialization
    protected void Start () {
        base.Start();
        _rotScript = GetComponent<AutoMoveAndRotate>();


        StartCoroutine("RandomSecUpdate");

    }

    float nextRand= 2;
    private IEnumerator RandomSecUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(nextRand); // wait
            _enemy = Program.GameScene.EnemyManager.GiveMeClosestEnemy(transform.position, IsGood);
            nextRand = UMath.GiveRandom(2, 3);

            if (_enemy!=null)
            {

            _enemyDist = Vector3.Distance(transform.position, _enemy.position);
            }

        }
    }

    // Update is called once per frame
    protected void Update () {


        if (_building != null && !_building.HasEnergy())
        {
            return;
        }

        if (_enemy != null && _enemyDist < 12)
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
