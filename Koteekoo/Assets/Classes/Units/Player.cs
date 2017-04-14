using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Shooter {

    float _speed = .1f;
    RaycastHit _hitMouseOnTerrain;
    Rigidbody _rigidBody;
    bool _isFalling;


    public bool IsMouseOnTerrain { get; private set; }

    public RaycastHit HitMouseOnTerrain
    {
        get
        {
            return _hitMouseOnTerrain;
        }

        set
        {
            _hitMouseOnTerrain = value;
        }
    }



    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        base.Start();
        Health = 10;
        IsGood = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Power <= 0)
        {
            Debug.Log("Game Over");
            return;
        }

        UpdateHitMouseOnTerrain();
        Movement();
        LookAtMousePos();

        Shoot();
        Jump();
    }





    private void Jump()
    {
        if (Input.GetKeyDown("space")  && !_isFalling)
        {
            _rigidBody.AddForce(new Vector3(0, 9, 0), ForceMode.Impulse);
        }

        _isFalling = true;
    }

    void OnCollisionStay(Collision collisionInfo)
    {


        //we are on something
        _isFalling = false;
    }


    void Movement()
    {
        float v = Input.GetAxis("Vertical") * _speed;
        float h = Input.GetAxis("Horizontal") * _speed;
        transform.position += new Vector3(h, 0, v);
    }

    private void LookAtMousePos()
    {
        transform.LookAt(HitMouseOnTerrain.point);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    public void UpdateHitMouseOnTerrain()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        // This would cast rays only against colliders in layer 8.
        int layerMask = 1 << 8;
        // Does the ray intersect any objects in the layer 8 "Terrain Layer"
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hitMouseOnTerrain,
            Mathf.Infinity, layerMask))
        {
            IsMouseOnTerrain = true;
        }
        else
        {
            //Debug.Log("Mouse Did not Hit Layer 8: Terrain");
            IsMouseOnTerrain = false;
        }

        //Debug.Log(HitMouseOnTerrain.collider.gameObject.name);
    }


    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<EnemyGO>();
            if (enemy.IsDeath())
            {
                collision.gameObject.transform.parent = BulletSpawn.transform;
                collision.gameObject.transform.position = BulletSpawn.transform.position;

            }
        }
    }


}
