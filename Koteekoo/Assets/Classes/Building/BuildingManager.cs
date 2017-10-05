﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : General
{
    Building current;

    List<Building> _allBuildings = new List<Building>();

    int _energyGen;
    int _energySpent;
    private Btn_Card _card;

    // Use this for initialization
    void Start()
    {
        _card = FindObjectOfType<Btn_Card>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create(string buildingPath)
    {
        //buildling path ex: Militar/Small_Wall
        var key = buildingPath.Split('/').ToArray()[1];
        if (!Building.DoWeHavePowerToBuildThis(key))
        {
            //negation sound
            Program.GameScene.SoundManager.PlaySound(7);
            return;    
        }

        _card.Hide();
        Program.GameScene.JoyStickManager.SetAsPlacingNow();
        current = Building.CreateB("Prefab/Building/" + buildingPath, ReturnInitialPosOfBuilding(),
            buildingPath, transform);
    }

    Vector3 ReturnInitialPosOfBuilding()
    {
        if (!Program.GameScene.JoyStickManager.JoyStickController)
        {
            return Program.GameScene.Player.HitMouseOnTerrain.point;
        }
        return Building.ReturnStaticLastPosIfAny();
    }

    public void AddToAll(Building build)
    {
        CheckTuto(build);
        _allBuildings.Add(build);
        current = null;
    }

    private void CheckTuto(Building build)
    {
        if (Program.GameScene.TutoWindow == null)
        {
            return;
        }

        if (Program.GameScene.TutoWindow.IsCurrentStep("Tuto.WallArdRocket"))
        {
            var howMany = _allBuildings.Where(a => a.name.Contains("Small_Wall")).Count();
            if (howMany >= 9)
            {
                Program.GameScene.TutoWindow.Next("Tuto.WallArdRocket");
            }
        }
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

    public bool IsBuildingNow()
    {
        return current != null;
    }

    internal int EnergySpent()
    {
        return _energySpent;
    }

    internal int EnergyGenerated()
    {
        return _energyGen;
    }

    public void AddToSpent(int pls)
    {
        _energySpent += pls;
    }

    public void AddToGen(int pls)
    {
        _energyGen += pls;
    }

    public void DestroyCurrentIfNoFixed()
    {
        if (current!=null && !current.WasFixed())
        {
            Destroy(current.gameObject);
        }
    }
}


