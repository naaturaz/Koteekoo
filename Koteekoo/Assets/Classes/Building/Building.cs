using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : General {

    bool _wasFixed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!_wasFixed)
        {
            transform.position = Program.GameScene.Player.HitMouseOnTerrain.point;
        }

        if (!_wasFixed && Input.GetMouseButtonUp(0))
        {
            _wasFixed = true;

            OwnTower();
        }
	}

    void OwnTower()
    {
        if (name.Contains("Own_Tower"))
        {

            var cellDroppedIn = Program.GameScene.Player.HitMouseOnTerrain.collider.gameObject.GetComponent<CellGO>();
            cellDroppedIn.ClaimedTerritory();
        }
    }
}
