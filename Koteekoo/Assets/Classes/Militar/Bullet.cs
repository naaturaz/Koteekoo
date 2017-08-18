using UnityEngine;
using System.Collections;
using System;

public class Bullet : General
{
    private float Range = 1.5f;
    public AudioClip ShootSound = null;
    private float _wasFiredAt = -1;

    private void Start()
    {
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (_wasFiredAt < 0)
        {
            return;
        }

        transform.position += transform.forward * 6.5f * Time.deltaTime;
        if (Time.time > _wasFiredAt + Range)
        {
            _wasFiredAt = -1;

            Program.GameScene.SpawnPool.AddToPool<Bullet>(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _wasFiredAt = -1;

        Program.GameScene.SpawnPool.AddToPool<Bullet>(this);

        //Destroy(gameObject);
        return;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);

    }

    internal void Fire(float bulletForce, bool isGood, Quaternion rotation)
    {
        IsGood = isGood;
        transform.rotation = rotation;
        _wasFiredAt = Time.time;
    }
}
