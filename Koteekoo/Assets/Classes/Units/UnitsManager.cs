using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour {

    List<Unit> _units = new List<Unit>();

    public List<Unit> Units
    {
        get
        {
            return _units;
        }

        set
        {
            _units = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create(string buildingPath)
    {
        var u = Unit.CreateU("Prefab/Units/" + buildingPath, Program.GameScene.Player.HitMouseOnTerrain.point, buildingPath, transform);

        //add to cell
        Units.Add(u);
    }
}
