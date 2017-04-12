using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour {


    static public General Create(string root, Vector3 origen, string name, Transform container = null)
    {
        General obj = null;
        obj = (General)Resources.Load(root, typeof(General));
        obj = (General)Instantiate(obj, origen, Quaternion.identity);
        obj.transform.name = name;

        if (container != null) { obj.transform.SetParent(container); }
        return obj;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// Get the child obj called "" in this Transform
    /// </summary>
    /// <returns></returns>
    protected GameObject GetChildCalled(string childName)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == childName)
            {
                return gameObject.transform.GetChild(i).gameObject;
            }
        }
        //print("Obj doesnt have a child called: " + childName );
        return null;
    }

    internal static EnemyGO Create(object p, Vector3 vector3, string v)
    {
        throw new NotImplementedException();
    }
}
