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
    protected int _health = 5;


    protected void Start()
    {
        _bulletSpawn = GetChildCalled("Bullet_Spawn");
        _bullet = (GameObject)Resources.Load("Militar/Bullet");
    }

    protected void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > _fireTime)
        {
            GameObject bullet = Instantiate(_bullet, _bulletSpawn.transform.position, _bulletSpawn.transform.rotation);
            bullet.name = "Bullet";
            _fireTime = Time.time + _fireRate;
        }
    }

    protected void ShootEnemy()
    {
        if (Time.time > _fireTime)
        {
            GameObject bullet = Instantiate(_bullet, _bulletSpawn.transform.position, _bulletSpawn.transform.rotation);
            bullet.name = "Bullet";
            _fireTime = Time.time + _fireRate;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.name != "Bullet")
        {
            return;
        }

        if (_health > 1)
        {
            _health--;
            //GetComponent<ParticleSystem>().Play();
            //GetComponent<AudioSource>().PlayOneShot(bloodSplat, 0.1f);

        }
        else
        {
            //GetComponent<Collider>().enabled = false;
            if (_health == 1)
            {
                _health = 0;
                //GetComponent<AudioSource>().PlayOneShot(deathSound, 1);
            }

            Destroy(gameObject, 2);
        }
    }
}



