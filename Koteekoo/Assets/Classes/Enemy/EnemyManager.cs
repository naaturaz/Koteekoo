using System;
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

    Vector3 _spawnPos;
    int _xSing;
    int _zSign;

    int _waveNumb = 1;
    int _kills;

    float _thisGameStartedAt;

    // Use this for initialization
    void Start()
    {
        _thisGameStartedAt = Time.time;
        _xSing = UMath.RandomSign();
        _zSign = UMath.RandomSign();

        var currLevel = PlayerPrefs.GetInt("Current");

        _nextWaveAt = Time.time + 20 + currLevel;
        //if (LoadSave.ThereIsALoad())
        //{
        //    SetNextWave();
        //    //load enemies

        //}
    }

    internal bool ThereIsAnAttackNow()
    {
        return _enemies.Count > 0;
    }

    // Update is called once per frame
    void Update()
    {
        //player should not be on air other wise nav weird message 
        if (Time.time > _nextWaveAt)
        {


            SetNextWave();
            _nextWaveEnemies = _waveNumb * (UMath.GiveRandom(1 + Program.GameScene.Level, 4 + Program.GameScene.Level));
            SpawnEnemies();
            Program.GameScene.CameraK.Attack();

            _waveNumb++;

        }
    }

    void SetNextWave()
    {
        //var randCap = 60 - Program.GameScene.Level;
        //if (randCap < 22)
        //{
        //    randCap = 22;
        //}
        //_nextWaveAt = Time.time + UMath.GiveRandom(20, randCap);

        _nextWaveAt = Time.time + 30 + Program.GameScene.Level;
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


        var _spawnPos = Program.GameScene.Player.transform.position +
            new Vector3(_xSing * 16, 0, _zSign * 16);

        var enemyNumb = UMath.GiveRandom(1, 3);
        var ene = (EnemyGO)General.Create("Prefab/Enemy/" + _enemyType + "/" + enemyNumb, _spawnPos, _enemyType + ".Enemy."+ enemyNumb);

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
        var index = _enemies.FindIndex(a => a == enemyGO);

        _enemies.RemoveAt(index);
        _enemiesTransform.RemoveAt(index);

        _kills++;

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

    public string NextWaveAt()
    {
        var sec = _nextWaveAt - Time.time;



        return GameScene.TimeFormat((int)sec);
    }

    public bool ToNextLevelIsReady()
    {
        var sec = _nextWaveAt - Time.time;
        //there is more time to the next wave than final time of level
        if (sec > Program.GameScene.TimeLeft1 && !ThereIsAnAttackNow())
        {
            return true;
        }
        return false;
    }

    internal float TtlTimeOfCurrentGame()
    {
        return Time.time - _thisGameStartedAt;
    }

    internal int Kills()
    {
        return _kills;
    }
}
