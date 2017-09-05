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
        //x axis left stick
        if (Input.GetAxis("HorizontalTurn") != 0 || Input.GetAxis("VerticalTurn") != 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("VerticalTurn"), Input.GetAxis("HorizontalTurn")) * 180 / Mathf.PI, 0);
            //Debug.Log("H:" + Input.GetAxis("HorizontalTurn"));
            //Debug.Log("V:" + Input.GetAxis("VerticalTurn"));
        }



    }
}
