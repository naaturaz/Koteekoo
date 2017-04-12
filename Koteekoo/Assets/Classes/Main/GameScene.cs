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

    public void Start()
    {
        Player = GameObject. FindObjectOfType<Player>();


        _terrainManager = new TerrainManager();
        _terrainManager.Start();


    }

    void Update()
    {
        
    }
}

