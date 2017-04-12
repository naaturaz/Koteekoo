using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    float _nextWaveAt;
    int _nextWaveEnemies;
    string _enemyType;

    // Use this for initialization
    void Start()
    {
        SetNextWave();

        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextWaveAt)
        {
            SetNextWave();
            _nextWaveEnemies = UMath.GiveRandom(1, 3);

            SpawnEnemies();
        }
    }

    void SetNextWave()
    {
        _nextWaveAt = Time.time + UMath.GiveRandom(10, 30);
        _enemyType = _posEnemies[UMath.GiveRandom(0, _posEnemies.Count)];
    }


    void SpawnEnemies()
    {

        for (int i = 0; i < _nextWaveEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    List<string> _posEnemies = new List<string>() { "Person", "Robot" };
    void SpawnEnemy()
    {

        var ene = (EnemyGO)General.Create("Prefab/Enemy/"+ _enemyType + "/" + 1,
            new Vector3(5, 0.519f, 5), "");

    }
}
