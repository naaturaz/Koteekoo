using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class CameraK : MonoBehaviour
{

    GameObject _cam_Point_50_Degrees;
    GameObject _cam_Point_90_Degrees;
    GameObject _target;

    float _speed = 2f;

    // Use this for initialization
    void Start()
    {
        _cam_Point_50_Degrees = GameObject.Find("Cam_Point_50_Degrees");
        _cam_Point_90_Degrees = GameObject.Find("Cam_Point_90_Degrees");

        _target = _cam_Point_50_Degrees;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        //transform.LookAt(Program.GameScene.Player.transform.position);
    }

    public void Attack()
    {
        _target = _cam_Point_90_Degrees;
        Program.GameScene.SoundManager.PlaySound(5);
        Program.GameScene.BuildingManager.DestroyCurrentIfNoFixed();
        _speed = 6f;
    }

    public void Peace()
    {
        _target = _cam_Point_50_Degrees;
        _speed = 2f;
    }
}
