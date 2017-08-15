using UnityEngine;
using System.Collections;
using System;

public class Bullet : General
{
    private float Force = 0;
    private float Range = 1.5f;
    public AudioClip ShootSound = null;
    private bool canMove;
    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            canMove = false;
            Destroy(_rigidBody);
        }
        transform.position += transform.forward * 4.5f * Time.deltaTime;
        Destroy(gameObject, Range);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        return;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);

    }

    internal void Fire(float bulletForce, bool isGood, Quaternion rotation)
    {
        IsGood = isGood;
        Force = bulletForce;
        canMove = true;

        transform.rotation = rotation;

    }
}
