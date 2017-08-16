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
    float _agentIniSpeed;

    public bool DebugWalk;
    int _leftRewards;
    AutoMoveAndRotate _rotScript;

    GameObject _marker;


    GameObject _rocket;
    bool _didTargetRocket;
    Vector3 _targetPos;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("OneSecUpdate");

        _rocket = GameObject.Find("Rocket");

        _agent = GetComponent<NavMeshAgent>();
        _agentIniSpeed = _agent.speed;

        _rotScript = GetComponent<AutoMoveAndRotate>();
        _rotScript.enabled = false;

        BulletForce = 1000;

        base.Start();
        _stump = GetChildCalled("Stump");
        _marker = GetChildCalled("Marker");

        _stump.SetActive(false);
        Ammo = 200;

        //Health = 6;
        if (name.Contains("2"))
        {
            Health = 20;
            FireRate = 3;
        }

        StartTargetAdquired();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnReward();

        if (Health == 0)
        {
            Destroy(gameObject, 15);
            return;
        }

        transform.LookAt(_targetPos);
        ShootEnemy();


        _agent.destination = _targetPos;

        CheckIfPaused();

    }

    private void CheckIfPaused()
    {
        if (Program.GameScene.JoyStickManager.ShouldStopPlayerMovement())
        {
            _agent.speed = 0;
        }
        else
        {
            _agent.speed = _agentIniSpeed;
        }
    }

    private IEnumerator OneSecUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // wait
            CheckOnAgentAndTarget();
        }
    }

    private void CheckOnAgentAndTarget()
    {
        if (Health == 0)
        {
            return;
        }

        if (_didTargetRocket )
        {
            return;
        }

        var dist = Vector3.Distance(transform.position, _rocket.transform.position);
        if (dist > 40)
        {
            TargetRocket();
            return;
        }

        _targetPos = Program.GameScene.Player.transform.position;
    }

    /// <summary>
    /// 5 % of them will get rocket as Target
    /// </summary>
    void StartTargetAdquired()
    {
        if (UMath.GiveRandom(1, 101) > 50)//or 20 % of the time 
        {
            TargetRocket();
        }
    }

    void TargetRocket()
    {
        _didTargetRocket = true;
        _targetPos = _rocket.transform.position;
        _agent.destination = _targetPos;

        Debug.Log("Target Rocket");
        return;
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

            _leftRewards = 1;
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
