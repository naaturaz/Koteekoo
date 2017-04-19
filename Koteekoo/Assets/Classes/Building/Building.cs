using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Building : Shooter
{

    bool _wasFixed;
    string _root;
    string _name;

    float _prodRate = 10f;
    float _lastProd;

    General _crate;
    string _inputCrate;
    bool _hasInput;

    bool _hasEnergy;
    float _energyLast = 30f;
    float _energyStart;


    //Rotating childs
    public AutoMoveAndRotate _rotator1;
    public AutoMoveAndRotate _rotator2;


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

    /// <summary>
    /// The name of this 
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public General Crate
    {
        get
        {
            return _crate;
        }

        set
        {
            _crate = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        base.Start();

        var arr = _root.Split('/');
        Name = arr[arr.Length - 1];

        if (IsAInputBuilding(name))
        {
            _inputCrate = "Miner_Crate";
        }

        ChangeRotatorsToState(_hasEnergy);
    }

    // Update is called once per frame
    void Update()
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

  
        if (Time.time > _energyStart + _energyLast && _hasEnergy)
        {
            _hasEnergy = false;
            ChangeRotatorsToState(false);
        }

        if (name.Contains("Solar_Panel"))
        {
            _hasEnergy = true;
        }


        Production();
    }

    void ChangeRotatorsToState(bool isOn)
    {
        if (_rotator1!=null)
        {
            _rotator1.enabled = isOn;
        }
        if (_rotator2 != null)
        {
            _rotator2.enabled = isOn;
        }
    }


    private void Production()
    {
        if (!_hasEnergy)
        {
            return;
        }

        var goodInput = _hasInput || !IsAInputBuilding(name);

        if (!_wasFixed || Time.time < _lastProd + _prodRate || Crate != null || !goodInput)
        {
            return;
        }

        var isProd = IsAProductionBuilding(name);
        if (isProd)
        {
            Produce();
        }
    }

    public static bool IsAProductionBuilding(string transfName)
    {
        return transfName.Contains("Bullets_Factory") || transfName.Contains("Parts_Factory")
            || transfName.Contains("Miner")
            || transfName.Contains("Solar_Panel");
    }

    public static bool IsAInputBuilding(string transfName)//
    {


        return transfName.Contains("Bullets_Factory") || transfName.Contains("Parts_Factory") ||
            transfName.Contains("Rocket_Base");
    }

    private void Produce()
    {
        var root = "Prefab/Crate/" + Name + "_Crate";
        _lastProd = Time.time;
        _hasInput = false;
        Crate = General.Create(root, transform.position + new Vector3(0, 4, 0f), root);
    }

    internal static Building CreateB(string root, Vector3 point, string buildingPath, Transform transform)
    {
        var obj = (Building)Create(root, point, buildingPath, transform);
        obj.Root = root;
        return obj;
    }

    void OwnTower()
    {
        if (name.Contains("Own_Tower"))
        {
            var cellDroppedIn = Program.GameScene.Player.HitMouseOnTerrain.collider.gameObject.GetComponent<CellGO>();

            //building second tower on same cell 
            if (cellDroppedIn == null)
            {
                return;
            }

            cellDroppedIn.ClaimedTerritory();
        }
    }


    #region Energy

    public bool HasEnergy()
    {
        return _hasEnergy;
    }

    void AddEnergy()
    {
        _hasEnergy = true;
        _energyStart = Time.time;
        ChangeRotatorsToState(true);

    }


    #endregion


    internal void AddPerson(EnemyGO enemyGO)
    {
        Debug.Log("p added");
        enemyGO.transform.parent = transform;
        enemyGO.transform.position = transform.position;

        Program.GameScene.Player.EmptyHands();
    }

    internal bool IsThisInputYouNeed(General onHands)
    {
        //solar panel doesnt need input 
        if (name.Contains("Solar_Panel"))
        {
            return false;
        }

        if (name.Contains("Rocket_Base"))
        {
            return true;
        }

        //carrying a soldier 
        if (onHands == null)
        {
            return false;
        }

        if (onHands.name.Contains("Solar_Panel_Crate"))
        {
            return true;
        }
        return onHands.name.Contains(_inputCrate);
    }

    internal void AddInput(General onHands)
    {
        if (onHands.name.Contains("Solar_Panel_Crate"))
        {
            AddEnergy();
            return;
        }

        _hasInput = true;
    }

    internal void Load(BuildingData data)
    {
        transform.position = data.Position;
        _wasFixed = true;
    }

    internal bool HasProductionOnStock()
    {
        return Crate != null;
    }

    internal void ClearBuildProd()
    {
        Crate = null;
    }
}


public class BuildingData
{
    public Vector3 Position;
    public string Root;
    public string Name;

    public BuildingData() { }

    public BuildingData(Vector3 position, string root)
    {
        Position = position;
        Root = root;
    }
}