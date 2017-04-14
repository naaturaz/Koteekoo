using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Cell
{
    Vector2 _nominalPos;//for ex the first cell is 0,0//also can be used as an id is an unique identifier
    Vector3 _position;
    string _root;

    List<Cell> _siblings = new List<Cell>();
    List<Vector2> _siblingsNominalPos = new List<Vector2>();

    CellGO _cellGO;

    public Vector2 NominalPos
    {
        get
        {
            return _nominalPos;
        }

        set
        {
            _nominalPos = value;
        }
    }

    public string Root
    {
        get
        {
            return _root;
        }

        set
        {
            _root = value;
        }
    }

    public Cell() { }

    public Cell(Vector2 nominalPos)
    {
        Root = "Prefab/Terrain/Cell" + UMath.GiveRandom(1,2);

        _nominalPos = nominalPos;
        _position = new Vector3(_nominalPos.x * 40, 0, _nominalPos.y * 40);

        _cellGO = CellGO.Create(Root, _position, Root, Program.GameScene.Container.transform, this);
    }

    public void SpawnMe()
    {
        _position = new Vector3(_nominalPos.x * 40, 0, _nominalPos.y * 40);
        _cellGO = CellGO.Create(Root, _position, Root, Program.GameScene.Container.transform, this);
    }



    public void CreateAllSiblings()
    {
        if (_siblings.Count > 0)
        {
            return;
        }
        CreateNominalPos();
        CreateCell();
    }

    private void CreateCell()
    {
        for (int i = 0; i < _siblingsNominalPos.Count; i++)
        {
            if (!Program.GameScene.TerrainManager.ExistCell(_siblingsNominalPos[i]))
            {
                //create this new cell with random root 
                var newCell = new Cell(_siblingsNominalPos[i]);

                _siblings.Add(newCell);
                Program.GameScene.TerrainManager.AddCell(newCell);
            }
        }
    }

    void CreateNominalPos()
    {
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                Vector2 newNominalPos = new Vector2(NominalPos.x + x, NominalPos.y + y);

                if (newNominalPos != NominalPos)
                {
                    _siblingsNominalPos.Add(newNominalPos);
                }
            }
        }
    }


}

