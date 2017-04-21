using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Shooter : General
{
    private float _fireTime;
    private float _fireRate = .2f;
    GameObject _bulletSpawn;
    GameObject _bullet;
    private int _health = 5;
    float _bulletForce = 1500;

    int _ammo = 200;
    public int Ammo
    {
        get
        {
            return _ammo;
        }

        set
        {
            _ammo = value;
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }

    public GameObject BulletSpawn
    {
        get
        {
            return _bulletSpawn;
        }

        set
        {
            _bulletSpawn = value;
        }
    }

    public float BulletForce
    {
        get
        {
            return _bulletForce;
        }

        set
        {
            _bulletForce = value;
        }
    }

    protected void Start()
    {
        base.Start();
        BulletSpawn = GetChildCalled("Bullet_Spawn");
        if (BulletSpawn == null)
        {
            BulletSpawn = GetGrandChildCalled("Bullet_Spawn");

            //a building that doesnt shoot 
            if (BulletSpawn == null)
            {
                return;
            }
        }

        _bullet = (GameObject)Resources.Load("Militar/Bullet");
    }

    protected void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > _fireTime)
        {
            SpawnBullet();
        }
    }

    protected void ShootEnemy()
    {
        if (Time.time > _fireTime)
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        if (_ammo < 1)
        {
            return;
        }

        GameObject bullet = Instantiate(_bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        bullet.name = "Bullet";
        _fireTime = Time.time + _fireRate;
        bullet.GetComponent<Bullet>().Fire(BulletForce, IsGood);
        //_ammo--;
    }

    protected void OnTriggerEnter(Collider other)
    {
        //if (other.name == "Player" && Unit.IsAnUnit(name))
        //{
        //    if (Program.GameScene.Player.Ammo > 200)
        //    {
        //        Ammo += 100;
        //        Program.GameScene.Player.Ammo -= 100;
        //        Debug.Log("100 added to: " + name);
        //    }
        //}

        if (other.name != "Bullet")
        {
            return;
        }

        var bulletComponent = other.gameObject.GetComponent<Bullet>();
        //so friendly fire doesnt affect units  
        if (IsGood == bulletComponent.IsGood)
        {
            return;
        }




        if (Health > 1)
        {
            Health--;
            //GetComponent<ParticleSystem>().Play();
            //GetComponent<AudioSource>().PlayOneShot(bloodSplat, 0.1f);
        }
        else
        {
            //GetComponent<Collider>().enabled = false;
            if (Health == 1)
            {
                Health = 0;
                //GetComponent<AudioSource>().PlayOneShot(deathSound, 1);
            }

            //Destroy(gameObject, 60);
        }
    }

    internal bool IsDeath()
    {
        return _health == 0;
    }


}



