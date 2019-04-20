using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickPlayerController : MonoBehaviour
{
    float _speed = 2;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine("WaitAlmostASec");

    }

    //private IEnumerator WaitAlmostASec()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(.05f);
    //        //to correct if hit a trees and rigid body wants to fall
    //        if (transform.rotation.x != 0)
    //        {
    //            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    //        }

    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //right stick
        if (Input.GetAxis("HorizontalTurn") != 0 || Input.GetAxis("VerticalTurn") != 0)
        {
            var direction = new Vector3(Input.GetAxis("HorizontalTurn"), 0, Input.GetAxis("VerticalTurn")); // set direction
            //so if left sticker is released will exit here...
            //will only pass if really was pressed by user
            if (direction.sqrMagnitude < 0.2f)
            {
                return;
            }

            transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("VerticalTurn"), Input.GetAxis("HorizontalTurn")) * 180//180
                / Mathf.PI, 0);
            //Debug.Log("H:" + Input.GetAxis("HorizontalTurn"));
            //Debug.Log("V:" + Input.GetAxis("VerticalTurn"));
        }

    }
}
