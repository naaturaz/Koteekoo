using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHead : Unit {

    float _speed = 20f;


    //values that will be set in the Inspector
    public Transform Target;
    public float RotationSpeed = 10;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Use this for initialization
    void Start () {
        base.Start();
        IsGood = true;
        Health = 100;
	}




    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (_enemy != null)
        {
            Target = _enemy;

            SlerpRot();
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, _enemy.rotation, _speed);

            //transform.LookAt(_enemy);
            ShootEnemy();
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
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
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





 
public class SlerpToLookAt : MonoBehaviour
{

}