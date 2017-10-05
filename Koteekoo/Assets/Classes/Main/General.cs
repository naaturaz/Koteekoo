using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour {


    bool _isGood;
    int _power = 100;

    /// <summary>
    /// Everything is good or bad, people and bullets
    /// </summary>
    public bool IsGood
    {
        get
        {
            return _isGood;
        }

        set
        {
            _isGood = value;
        }
    }

    /// <summary>
    /// Everyone has power, units, buildings 
    /// </summary>
    public int Power
    {
        get
        {
            return _power;
        }

        set
        {
            _power = value;
        }
    }


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
    protected void Start () {
        StartCoroutine("OneSecUpdate");
    }


    private IEnumerator OneSecUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(3); // wait

            if (Power > 0)
            {
                //Power--;
            }
        }
    }

    internal void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Current", 0);
        PlayerPrefs.SetString("State", "New");

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


    /// <summary>
    /// Get the grand child obj called "" in this Transform
    /// </summary>
    /// <returns></returns>
    protected GameObject GetGrandChildCalled(string grandName)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i);
            for (int j = 0; j < child.transform.childCount; j++)
            {

                var grandChild = child.transform.GetChild(j).gameObject;
                if (grandChild.name == grandName)
                {
                    return grandChild;
                }

            }

        }
        //print("Obj doesnt have a child called: " + childName );
        return null;
    }

    public static List<GameObject> GetAllChilds(GameObject gO)
    {
        List<GameObject> res = new List<GameObject>();
        for (int i = 0; i < gO.transform.childCount; i++)
        {

            res.Add(gO.transform.GetChild(i).gameObject);

        }
        return res;
    }


}
