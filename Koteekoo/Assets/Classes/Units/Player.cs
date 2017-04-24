﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Shooter
{

    float _speed = .05f;//.1
    RaycastHit _hitMouseOnTerrain;
    Rigidbody _rigidBody;
    bool _isFalling;
    bool _hasHandOccupied;
    General _onHands;

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

    public bool IsFalling
    {
        get
        {
            return _isFalling;
        }

        set
        {
            _isFalling = value;
        }
    }



    // Use this for initialization
    void Start()
    {
        IsGood = true;

        _rigidBody = GetComponent<Rigidbody>();
        base.Start();
        Health = 10;
        Ammo = 2000;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath())
        {
            GameOver("Player was killed");
            return;
        }

        UpdateHitMouseOnTerrain();
        Movement();
        LookAtMousePos();

        Shoot();
        Jump();

        UnableRigidIfBuilding();
    }

    public void GameOver(string reason)
    {
        Debug.Log("Game Over");
        Application.LoadLevel("MainMenu");
        PlayerPrefs.SetString("State", "GameOver");//Clear current game 
        PlayerPrefs.SetString("Reason", reason); 

    }

    private void UnableRigidIfBuilding()
    {
        if (Program.GameScene.BuildingManager.IsBuildingNow())
        {
            _rigidBody.isKinematic = true;
        }
        else
        {

            _rigidBody.isKinematic = false;

        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown("space") && !IsFalling)
        {
            _rigidBody.AddForce(new Vector3(0, 9, 0), ForceMode.Impulse);
        }

        IsFalling = true;
    }

    void OnCollisionStay(Collision collisionInfo)
    {


        //we are on something
        IsFalling = false;
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



    #region Pick Up

    void OnCollisionEnter(Collision collision)
    {
        if (_hasHandOccupied)
        {
            //HandleTouchInputBuilding(collision);
            return;
        }

        //HandleTouchEnemy(collision);
        //HandleTouchProdBuilding(collision);
        //HandleTouchUnit(collision);
    }

    private void HandleTouchUnit(Collision collision)
    {
        //if (Unit.IsAnUnit(collision.gameObject.name))
        //{
        //    if (Ammo > 200)
        //    {

        //    }
        //}
    }

    private void HandleTouchInputBuilding(Collision collision)
    {
        if (Building.IsAInputBuilding(collision.gameObject.name) ||
            (_onHands != null && _onHands.name.Contains("Solar_Panel_Crate")))
        {
            var build = collision.gameObject.GetComponent<Building>();
            if (build == null)
            {
                return;
            }

            if (build.IsThisInputYouNeed(_onHands))
            {
                build.AddInput(_onHands);
                Destroy(_onHands.gameObject);
                _hasHandOccupied = false;
            }
        }
    }

    private void HandleTouchProdBuilding(Collision collision)
    {
        if (Building.IsAProductionBuilding(collision.gameObject.name))
        {
            var build = collision.gameObject.GetComponent<Building>();
            if (build.HasProductionOnStock())
            {
                if (build.Crate.name.Contains("Bullet"))
                {
                    Destroy(build.Crate.gameObject);
                    build.ClearBuildProd();
                    Ammo += 100;
                    return;
                }

                build.Crate.transform.parent = BulletSpawn.transform;
                build.Crate.transform.position = BulletSpawn.transform.position;
                _hasHandOccupied = true;
                _onHands = build.Crate;
                build.ClearBuildProd();
            }
        }


    }

    internal int EnergyGenerated()
    {
        throw new NotImplementedException();
    }



    void HandleTouchEnemy(Collision collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<EnemyGO>();
            if (enemy.IsDeath())
            {
                collision.gameObject.transform.parent = BulletSpawn.transform;
                collision.gameObject.transform.position = BulletSpawn.transform.position;
                _hasHandOccupied = true;
            }
        }
    }

    internal void EmptyHands()
    {
        _hasHandOccupied = false;
    }


    #endregion


}