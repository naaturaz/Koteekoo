using System;
using UnityEngine;


public class FollowTargetPlayer : General
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    public bool IsToFollowPlayer;
    public float Speed = 0.1f;

    public bool ContraintY;

    Vector3 _initPos;

    private void Start()
    {
        _initPos = transform.position;


        if (IsToFollowPlayer)
        {
            target = GameObject.Find("Player").transform;
        }

    }

    private void LateUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, Speed);

        if (ContraintY)
        {
            transform.position = new Vector3(transform.position.x, _initPos.y, transform.position.z);

        }

    }

}
