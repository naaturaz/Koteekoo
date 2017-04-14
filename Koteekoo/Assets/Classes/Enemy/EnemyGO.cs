using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyGO : Shooter
{
    float _speed = 0.04f;
    GameObject _stump;
    NavMeshAgent _agent;

    public bool DebugWalk;

    // Use this for initialization
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        BulletForce = 1000;

        base.Start();
        _stump = GetChildCalled("Stump");
        _stump.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health == 0)
        {
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

    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (Health == 0 && _agent.enabled)
        {
            _stump.SetActive(true);
            _agent.enabled = false;
            Program.GameScene.EnemyManager.RemoveMeFromEnemiesList(this);
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


}
