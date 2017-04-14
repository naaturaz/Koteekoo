using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    TerrainManager _terrainManager;
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

    List<BuildingData> _allBuilds = new List<BuildingData>();

    PlayerSavedData _player;

    public PlayerSavedData Player
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

    public List<BuildingData> AllBuilds
    {
        get
        {
            return _allBuilds;
        }

        set
        {
            _allBuilds = value;
        }
    }

    public Data() { }

    public Data(bool saveNow) {
        Save();
    }


    public void Save()
    {
        TerrainManager = Program.GameScene.TerrainManager;
        Player = new PlayerSavedData(true);
        _allBuilds = Program.GameScene.BuildingManager.GetAllBuilds();
    }

    public void Load()
    {
        Program.GameScene.TerrainManager = TerrainManager;
        Program.GameScene.TerrainManager.StartLoadedTerrain();
        Player.LoadData();

        Program.GameScene.BuildingManager.LoadAllBuilds(_allBuilds);
    }
}


public class PlayerSavedData
{
    public Vector3 Position;

    public PlayerSavedData() { }

    public PlayerSavedData(bool saveNow)
    {
        SaveData();
    }


    public void SaveData()
    {
        Position = Program.GameScene.Player.transform.position;
    }

    public void LoadData()
    {
        Program.GameScene.Player.transform.position = Position;

    }
}