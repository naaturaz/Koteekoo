using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class GameScene 
{
    Player _player;

    public Player Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }

    TerrainManager _terrainManager;
    GameObject _container;
    BuildingManager _buildingManager;
    EnemyManager _enemyManager;
    UnitsManager _unitsManager;

    public GameScene()
    {
    }

    public TerrainManager TerrainManager
    {
        get
        {
            return _terrainManager;
        }

        set
        {
            _terrainManager = value;
        }
    }

    public GameObject Container
    {
        get
        {
            return _container;
        }

        set
        {
            _container = value;
        }
    }

    public BuildingManager BuildingManager
    {
        get
        {
            return _buildingManager;
        }

        set
        {
            _buildingManager = value;
        }
    }

    public EnemyManager EnemyManager
    {
        get
        {
            return _enemyManager;
        }

        set
        {
            _enemyManager = value;
        }
    }

    public UnitsManager UnitsManager
    {
        get
        {
            return _unitsManager;
        }

        set
        {
            _unitsManager = value;
        }
    }

    public void Start()
    {
        Player = GameObject.FindObjectOfType<Player>();
        BuildingManager = GameObject.FindObjectOfType<BuildingManager>();
        EnemyManager = GameObject.FindObjectOfType<EnemyManager>();
        UnitsManager = GameObject.FindObjectOfType<UnitsManager>();

        if (LoadSave.ThereIsALoad())
        {
            LoadSave.LoadNow();
        }
        else
        {
            _terrainManager = new TerrainManager();
            _terrainManager.Start();
        }




    }

    void Update()
    {
        
    }
}

