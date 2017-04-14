using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Shooter {

    bool _wasFixed;
    string _root;

    public string Root
    {
        get
        {
            return _root;
        }

        set
        {
            _root = value;
        }
    }

    // Use this for initialization
    void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!_wasFixed)
        {
            transform.position = Program.GameScene.Player.HitMouseOnTerrain.point;
        }

        if (!_wasFixed && Input.GetMouseButtonUp(0))
        {
            _wasFixed = true;
            Program.GameScene.BuildingManager.AddToAll(this);
            OwnTower();
        }
	}

    internal static Building CreateB(string root, Vector3 point, string buildingPath, Transform transform)
    {
        var obj = (Building) Create(root, point, buildingPath, transform);
        obj.Root = root;
        return obj;
    }

    void OwnTower()
    {
        if (name.Contains("Own_Tower"))
        {
            var cellDroppedIn = Program.GameScene.Player.HitMouseOnTerrain.collider.gameObject.GetComponent<CellGO>();
            cellDroppedIn.ClaimedTerritory();
        }
    }


    internal void AddPerson(EnemyGO enemyGO)
    {
        Debug.Log("p added");
        enemyGO.transform.parent = transform;
        enemyGO.transform.position = transform.position;
    }

    internal void Load(BuildingData data)
    {
        transform.position = data.Position;
        _wasFixed = true;
    }
}


public class BuildingData
{
    public Vector3 Position;
    public string Root;

    public BuildingData() { }

    public BuildingData(Vector3 position, string root)
    {
        Position = position;
        Root = root;
    }
}