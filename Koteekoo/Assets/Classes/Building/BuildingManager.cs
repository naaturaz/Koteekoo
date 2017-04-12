using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : General
{

    Building current;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create(string buildingPath)
    {
        current = (Building)Building.Create("Prefab/Building/" + buildingPath, Program.GameScene.Player.HitMouseOnTerrain.point, buildingPath, transform);

        //add to cell


    }
}
