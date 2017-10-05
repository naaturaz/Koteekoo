using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class TurretHead : Unit
{
    float _speed = 20f;

    //values that will be set in the Inspector
    public Transform Target;
    public float TargetRotationSpeed = 10;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    AutoMoveAndRotate _rotScript;



    // Use this for initialization
    void Start()
    {
        IsGood = true;

        if (name.Contains("Enemy"))
        {
            IsGood = false;
            transform.parent.GetComponent<Building>().SetWasFixed(true);
        }

        base.Start();

        _building = transform.parent.gameObject.GetComponent<Building>();
        _rotScript = GetComponent<AutoMoveAndRotate>();
        Health = 50;

        //CreateHealthBar();
    }




    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (!_building.HasEnergy())
        {
            return;
        }

        if (_enemy != null)
        {
            Target = _enemy;
            SlerpRot();
        }
        else
        {
            //stand by
        }


    }

    void SlerpRot()
    {
        //find the vector pointing from our position to the target
        _direction = (Target.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * TargetRotationSpeed);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (IsDeath())
        {
            Destroy(transform.parent.gameObject);
        }

        base.OnTriggerEnter(other);
    }
}

