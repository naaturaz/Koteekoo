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
            _rigidBody.AddForce(transform.forward * Force, ForceMode.Acceleration);
            canMove = false;
        }

        Destroy(gameObject, Range);

    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
        return;



            //Destroy(gameObject, ShootSound.length);
        
    }

    internal void Fire(float bulletForce, bool isGood)
    {
        IsGood = isGood;
        Force = bulletForce;
        canMove = true;

    }
}
