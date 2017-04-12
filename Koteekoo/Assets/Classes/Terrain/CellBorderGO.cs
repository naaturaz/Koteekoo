using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBorderGO : MonoBehaviour {

    public CellGO CellGO1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Contains("Player"))
        {
            CellGO1.PlayerEnter();
        }


    }
}
