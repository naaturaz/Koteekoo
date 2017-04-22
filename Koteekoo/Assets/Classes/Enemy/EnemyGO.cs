using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using UnityStandardAssets.Utility;

public class EnemyGO : Shooter
{
    float _speed = 0.04f;
    GameObject _stump;
    NavMeshAgent _agent;

    public bool DebugWalk;
    int _leftRewards;
    AutoMoveAndRotate _rotScript;

    GameObject _marker;

    // Use this for initialization
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rotScript = GetComponent<AutoMoveAndRotate>();
        _rotScript.enabled = false; 

        BulletForce = 1000;

        base.Start();
        _stump = GetChildCalled("Stump");
        _marker = GetChildCalled("Marker");

        _stump.SetActive(false);
        Ammo = 200;

        if (name.Contains("2"))
        {
            Health = 20;
            FireRate = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnReward();

        if (Health == 0)
        {
            Destroy(gameObject, 10);
            return;
        }

        transform.LookAt(Program.GameScene.Player.transform);

        ShootEnemy();

        //transform.position = Vector3.MoveTowards(transform.position, Program.GameScene.Player.transform.position, _speed);

        //if (DebugWalk)
        //{
            _agent.destination = Program.GameScene.Player.transform.position;
        //}

        
    }

    int count;
    private void CheckOnReward()
    {
        if (_leftRewards == 0)
        {
            return;
        }

        if (count > 160)
        {
            Reward();
            _leftRewards--;
            count = 0;
        }
        count++;
    }

    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (Health == 0 && _agent.enabled)
        {
            _stump.SetActive(true);
            _marker.SetActive(false);
            _agent.enabled = false;
            Program.GameScene.EnemyManager.RemoveMeFromEnemiesList(this);
            _rotScript.enabled = true;

            _leftRewards = 2;
            Program.GameScene.SoundManager.PlaySound(4);

        }

        if (!IsDeath() || transform.parent == null)
        {
            return;
        }

        var personHosp = other.gameObject.name.Contains("Hospital") && name.Contains("Person");
        var botDepot = other.gameObject.name.Contains("Depot") && name.Contains("Robot");

        if (personHosp || botDepot)
        {
            AddMeToBuilding(other);
        }
    }

    void AddMeToBuilding(Collider other)
    {
        var building = other.gameObject.transform.parent.GetComponent<Building>();
        building.AddPerson(this);
    }

    private void Reward()
    {
        var root = "Prefab/Crate/Solar_Panel_Crate";
        var Crate = General.Create(root, transform.position, root);
    }
}
