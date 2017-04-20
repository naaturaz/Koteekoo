﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float _nextWaveAt;
    int _nextWaveEnemies;
    string _enemyType;

    List<EnemyGO> _enemies = new List<EnemyGO>();
    List<Transform> _enemiesTransform = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        _nextWaveAt = Time.time + 3;
        if (LoadSave.ThereIsALoad())
        {
            SetNextWave();
            //load enemies

        }
    }

    // Update is called once per frame
    void Update()
    {
        //player should not be on air other wise nav weird message 
        if (Time.time > _nextWaveAt)
        {
            SetNextWave();
            _nextWaveEnemies = UMath.GiveRandom(1 + Program.GameScene.Level, 4 + Program.GameScene.Level);
            SpawnEnemies();
            Program.GameScene.CameraK.Attack();
        }
    }

    void SetNextWave()
    {
        var randCap = 60 - Program.GameScene.Level;
        if (randCap < 22)
        {
            randCap = 22;
        }

        _nextWaveAt = Time.time + UMath.GiveRandom(20, randCap);
        _enemyType = _posEnemies[UMath.GiveRandom(0, _posEnemies.Count)];
    }


    void SpawnEnemies()
    {
        for (int i = 0; i < _nextWaveEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    List<string> _posEnemies = new List<string>() { "Person", //"Robot"
    };
    void SpawnEnemy()
    {
        if (Program.GameScene == null || Program.GameScene.Player == null)
        {
            return;
        }

        var pos = Program.GameScene.Player.transform.position + 
            new Vector3(UMath.RandomSign() * 12, 0, UMath.RandomSign() * 12);

        var ene = (EnemyGO)General.Create("Prefab/Enemy/"+ _enemyType + "/" + 1, pos, _enemyType + ".Enemy");

        _enemies.Add(ene);
        _enemiesTransform.Add(ene.transform);
    }


    public Transform GiveMeClosestEnemy(Vector3 from)
    {
        if (_enemies.Count == 0)
        {
            return null;
        }

        return GetClosestEnemy(_enemiesTransform, from);
    }

    internal void RemoveMeFromEnemiesList(EnemyGO enemyGO)
    {
        var index = _enemies.FindIndex(a=>a==enemyGO);

        _enemies.RemoveAt(index);
        _enemiesTransform.RemoveAt(index);

        if (_enemies.Count == 0)
        {
            Program.GameScene.CameraK.Peace();
        }
    }

    Transform GetClosestEnemy(List<Transform> enemies, Vector3 from)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, from);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
