using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Shooter : General
{

    GameObject _canvas;


    private float _fireTime;
    private float _fireRate = .2f;
    GameObject _bulletSpawn;
    //GameObject _bullet;
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

    static float _lastShoot;


    HealthBar _healthBar;


    public HealthBar HealthBar
    {
        get
        {
            return _healthBar;
        }

        set
        {
            _healthBar = value;
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

    public float FireRate
    {
        get
        {
            return _fireRate;
        }

        set
        {
            _fireRate = value;
        }
    }

    protected void Start()
    {
        base.Start();

        _canvas = GameObject.Find("Canvas");


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



    }

    protected void Shoot()
    {
        if ((Input.GetButton("Fire1") || Program.GameScene.EnemyManager.ThereIsAnAttackNow()) && Time.time > _fireTime)
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
        if (Program.GameScene.JoyStickManager.ShouldStopPlayerMovement() || _ammo < 1)
        {
            return;
        }

        //GameObject bullet = Instantiate(_bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);

        var kind = "";
        if (IsGood)
        {
            kind = "Bullet_Good";
        }
        else
        {
            kind = "Bullet_Bad";

        }

        var bull = Program.GameScene.SpawnPool.ReturnGeneral(kind);
        if (bull == null)
        {
            //Debug.Log("not :" + kind);
            return;
        }

        GameObject bullet = bull.gameObject;
        bullet.transform.position = _bulletSpawn.transform.position;

        _fireTime = Time.time + FireRate;
        bullet.GetComponent<Bullet>().Fire(BulletForce, IsGood, BulletSpawn.transform.rotation);

        if (!IsGood)
        {
            return;
        }

        Program.GameScene.SoundManager.PlaySound(0, 1, true);
    }


    protected void OnTriggerEnter(Collider other)
    {
        if (!other.name.Contains("Bullet"))
        {
            return;
        }

        var bulletComponent = other.gameObject.GetComponent<Bullet>();
        //so friendly fire doesnt affect units  
        if (IsGood == bulletComponent.IsGood)
        {
            return;
        }

        RemoveHealth();
    }

    void RemoveHealth()
    {
        if (Health > 1)
        {
            Health--;

            if (name.Contains("Player"))
            {
                var p = (Player)this;
                p.Hit();
            }
            else if (name.Contains("Enemy"))
            {
                var one = General.Create("Prefab/Crate/Diamond_Drop", transform.position, "Diamond_Drop");
                Program.GameScene.EnemyManager.AddDamageReceived();
            }
        }
        else
        {
            if (Health == 1)
            {
                Health = 0;
                if (name.Contains("Enemy") && UMath.GiveRandom(1, 11) < 2)//10% chance of drop 
                {
                    var heart = General.Create("Prefab/Crate/Heart_Drop", transform.position, "Heart_Drop");
                }
            }
        }
    }

    internal bool IsDeath()
    {
        return _health == 0;
    }


    /// <summary>
    /// Call me after health is being set up
    /// </summary>
    protected void CreateHealthBar(string barName = "")
    {
        var pos = transform.position + new Vector3(0, 1.4f, 0);
        if (name.Contains("Rocket"))
        {
            pos += new Vector3(0, 0.6f, 0);
        }

        HealthBar = (HealthBar)Create("Prefab/GUI/Health_Bar" + barName, pos, "H_Bar", transform);
        HealthBar.PassShooter(this);
    }


}



