using System;
using UnityEngine;


public class FollowTargetPlayer : General
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    public bool IsToFollowPlayer;
    public float Speed = 0.1f;


    private void Start()
    {
        if (IsToFollowPlayer)
        {
            target = GameObject.Find("Player").transform;
        }

    }


}
