using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //drag and drop gameObj arrows here 
    public GameObject[] Arrows = new GameObject[4];

    General _enemySpawnPoint;

    float _nextWaveAt;
    int _nextWaveEnemies;

    GameObject _rocket;

    List<EnemyGO> _enemies = new List<EnemyGO>();
    List<Transform> _enemiesTransform = new List<Transform>();

    Vector3 _spawnPos;
    int _xSing;
    int _zSign;

    int _waveNumb = 1;
    int _kills;

    float _thisGameStartedAt;

    private int _damageReceived;


    // Use this for initialization
    void Start()
    {
        _thisGameStartedAt = Time.time;

        _rocket = GameObject.Find("Rocket");

        if (PlayerPrefs.GetString("Tuto") == "" && Program.GameScene.Level == 1)
        {
            _xSing = 1;
            _zSign = 1;
        }
        else
        {
            _xSing = UMath.RandomSign();
            _zSign = UMath.RandomSign();
        }

        var currLevel = PlayerPrefs.GetInt("Current");
        _nextWaveAt = Program.GameScene.TimePass + 10 + currLevel;

        StartCoroutine("Wait2Sec");
        HideAllArrowsButNeededOne();
    }

    //what arrow GameObject each direction in x and z sign will talk to 
    Dictionary<string, int> _arrowsMap = new Dictionary<string, int>()
    {
        {"1,1",0 },
        {"-1,1",1 },
        {"-1,-1",2 },
        {"1,-1",3 },
    };
    private void HideAllArrowsButNeededOne()
    {
        if (Arrows[0] == null)
        {
            return;
        }

        for (int i = 0; i < Arrows.Length; i++)
        {
            Arrows[i].gameObject.SetActive(false);
        }
        var index = _arrowsMap[_xSing + "," + _zSign];
        Arrows[index].SetActive(true);
    }

    private IEnumerator Wait2Sec()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            SpawnEnemies();
        }
    }

    internal bool ThereIsAnAttackNow()
    {
        return _enemies.Count > 0;
    }

    // Update is called once per frame
    void Update()
    {
        //player should not be on air other wise nav weird message 
        if (Program.GameScene.TimePass > _nextWaveAt)
        {
            var levlDif = Program.GameScene.Level;

            SetNextWave();
            _waveNumb++;

            //will skip a wave if there is still an attack now 
            if (ThereIsAnAttackNow())
            {
                return;
            }

            count = 0;//triggers the start of spawning 
            _nextWaveEnemies = _waveNumb + (UMath.GiveRandom(levlDif, 1 + levlDif));
            Program.GameScene.CameraK.Attack();
        }
        //SpawnEnemies();
    }

    void SetNextWave()
    {
        _nextWaveAt = Program.GameScene.TimePass + 20;
    }


    int count = -1;
    void SpawnEnemies()
    {
        if (count > -1 && count < _nextWaveEnemies)
        {
            SpawnEnemy();
            count++;
        }
        else if (count >= _nextWaveEnemies)
        {
            count = -1;
        }
    }

    List<string> _enemiesList = new List<string>() { "Person", //"Robot"
    };

    void SpawnEnemy()
    {
        if (Program.GameScene == null || Program.GameScene.Player == null)
        {
            return;
        }

        var enemyType = _enemiesList[UMath.GiveRandom(0, _enemiesList.Count)];

        var spawnPos = _rocket.transform.position +
            new Vector3(_xSing * 20, 0, _zSign * 20);

        SpawnEnemySpawnPoint(spawnPos);

        var enemyNumb = UMath.GiveRandom(1, 3);
        var ene = (EnemyGO)General.Create("Prefab/Enemy/" + enemyType + "/" + enemyNumb, spawnPos, enemyType + ".Enemy." + enemyNumb);

        _enemies.Add(ene);
        _enemiesTransform.Add(ene.transform);
    }

    private void SpawnEnemySpawnPoint(Vector3 spawnPos)
    {
        if (_enemySpawnPoint == null)
        {
            _enemySpawnPoint = General.Create("Prefab/Enemy/Red_Circle", spawnPos
                , "Name Prefab/Enemy/Red_Circle");
            _enemySpawnPoint.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// Will return closest enemy. 
    /// If a bad is asking for a enemy will return player 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="isGood">if the one askign is good or not</param>
    /// <returns></returns>
    public Transform GiveMeClosestEnemy(Vector3 from, bool isGood)
    {
        if (!isGood)
        {
            return Program.GameScene.Player.transform;
        }

        if (_enemies.Count == 0)
        {
            return null;
        }

        return GetClosestEnemy(_enemiesTransform, from);
    }

    internal void AddDamageReceived()
    {
        _damageReceived++;
    }

    internal int DamageReceived()
    {
        return _damageReceived;
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
        var sec = _nextWaveAt - Program.GameScene.TimePass;
        return GameScene.TimeFormat((int)sec);
    }

    public bool ToNextLevelIsReady()
    {
        //var sec = _nextWaveAt - Time.time;
        ////there is more time to the next wave than final time of level
        //if (sec > Program.GameScene.TimeLeft1 && !ThereIsAnAttackNow())
        //{
        //    return true;
        //}
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

    /// <summary>
    /// Returns time from now to next wave
    /// </summary>
    /// <returns></returns>
    internal int TimeToNextWave()
    {
        return (int)_nextWaveAt - (int)Time.time;
    }
}
