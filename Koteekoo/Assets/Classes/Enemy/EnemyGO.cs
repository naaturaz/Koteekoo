using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGO : Shooter
{
    float _speed = 0.1f;

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Program.GameScene.Player.transform);

        ShootEnemy();
    }

    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Program.GameScene.Player.transform.position, _speed);
    }
}
