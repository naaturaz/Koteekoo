﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class GameScene
{
    int _timeLeft;
    Player _player;
    int _level;


    

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
    CameraK _cameraK;
    SoundManager _soundManager;

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

    public int Level
    {
        get
        {
            return _level;
        }

        set
        {
            _level = value;
        }
    }

    public CameraK CameraK
    {
        get
        {
            return _cameraK;
        }

        set
        {
            _cameraK = value;
        }
    }

    public int TimeLeft1
    {
        get
        {
            return _timeLeft;
        }

        set
        {
            _timeLeft = value;
        }
    }

    public SoundManager SoundManager
    {
        get
        {
            return _soundManager;
        }

        set
        {
            _soundManager = value;
        }
    }

    void LoadLevel()
    {
        Level = PlayerPrefs.GetInt("Current");
        
        if (Level == 0)
        {
            Level = 1;
        }
    }

    public void Start()
    {
        LoadLevel();

        Player = GameObject.FindObjectOfType<Player>();
        BuildingManager = GameObject.FindObjectOfType<BuildingManager>();
        EnemyManager = GameObject.FindObjectOfType<EnemyManager>();
        UnitsManager = GameObject.FindObjectOfType<UnitsManager>();
        CameraK = GameObject.FindObjectOfType<CameraK>();
        SoundManager = GameObject.FindObjectOfType<SoundManager>();


        if (Application.loadedLevelName == "MainMenu")
        {
            Program.GameScene.SoundManager.PlayMusic(0);
            return;
        }


        if (LoadSave.ThereIsALoad())
        {
            LoadSave.LoadNow();
        }
        else
        {
            _terrainManager = new TerrainManager();
            _terrainManager.Start();
        }

        DefineGameTimeLeft();
        DefinePowerAndInitValForLevel();

        if (Application.loadedLevelName == "Scn01")
        {
            Program.GameScene.SoundManager.PlayMusic(1);
        }
       
    }

    private void DefinePowerAndInitValForLevel()
    {
        Program.GameScene.Player.Power += (90 * Level);

    }

    private void DefineGameTimeLeft()
    {
        _timeLeft = 60 + (20*Level);
    }

    public void Update()
    {

    }

    internal void OneSecUpdate()
    {
        if (_timeLeft > 1)
        {
            _timeLeft--;
        }
        else
        {
            PassLevel();
        }
    }

    public void PassLevel()
    {
        SaveLevelStats();


        //player passed the level 
        Level++;
        PlayerPrefs.SetInt("Current", Level);//so from Scene to scene remembers aaaa
        PlayerPrefs.SetString("State", "Pass");//so from Scene to scene remembers aaaa

        Debug.Log("Level pass");
        Application.LoadLevel("MainMenu");

    }

    private void SaveLevelStats()
    {
        PlayerPrefs.SetInt("Enemy", EnemyManager.Kills());
        PlayerPrefs.SetInt("Generated", BuildingManager.EnergyGenerated());
        PlayerPrefs.SetInt("Spent", BuildingManager.EnergySpent());
        PlayerPrefs.SetFloat("Time", EnemyManager.TtlTimeOfCurrentGame());
        PlayerPrefs.SetInt("Health", Player.Health);

    }

    public string TimeLeft()
    {
        return TimeFormat(_timeLeft);
    }

    public static string TimeFormat(int sec)
    {
        TimeSpan span = new TimeSpan(0, 0, sec);

        var secSt = "";
        if (span.Seconds < 10)
        {
            secSt = "0" + span.Seconds;
        }
        else
        {
            secSt = span.Seconds + "";
        }


        return string.Format("{0}:{1}", span.Minutes, secSt);
    }
}

