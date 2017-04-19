using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : General
{
    Building current;

    List<Building> _allBuildings = new List<Building>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create(string buildingPath)
    {
        current = Building.CreateB("Prefab/Building/" + buildingPath, Program.GameScene.Player.HitMouseOnTerrain.point, buildingPath, transform);

        //add to cell
    }

    public void AddToAll(Building build)
    {
        _allBuildings.Add(build);
    }

    internal List<BuildingData> GetAllBuilds()
    {
        List<BuildingData> res = new List<BuildingData>();
        for (int i = 0; i < _allBuildings.Count; i++)
        {
            res.Add(new BuildingData(_allBuildings[i].transform.position, _allBuildings[i].Root));
        }
        return res;
    }


    internal void LoadAllBuilds(List<BuildingData> _allBuilds)
    {
        for (int i = 0; i < _allBuilds.Count; i++)
        {
            _allBuildings.Add(CreateFullPath(_allBuilds[i]));
        }
    }

    public Building CreateFullPath(BuildingData data)
    {
        var c = Building.CreateB(data.Root, Program.GameScene.Player.HitMouseOnTerrain.point, data.Root, transform);
        c.Load(data);

        return c;
    }

    internal void RemoveBuilding(Building build)
    {
        _allBuildings.Remove(build);
    }
}


