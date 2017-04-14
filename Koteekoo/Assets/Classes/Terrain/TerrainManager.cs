using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TerrainManager
{
    List<Cell> _allCells = new List<Cell>();

    public List<Cell> AllCells
    {
        get
        {
            return _allCells;
        }

        set
        {
            _allCells = value;
        }
    }

    public TerrainManager()
    {



    }

    public void Start()
    {
        Cell initCell = new Cell(new Vector2());
        Program.GameScene.TerrainManager.AddCell(initCell);

        initCell.CreateAllSiblings();
    }

    internal void StartLoadedTerrain()
    {
        for (int i = 0; i < _allCells.Count; i++)
        {
            _allCells[i].SpawnMe();
        }
    }

    public void AddCell(Cell cell)
    {
        AllCells.Add(cell);
    }

    public bool ExistCell(Vector2 nominalPos)
    {
        var found = AllCells.Find(a => a.NominalPos == nominalPos);
        return found != null;
    }


}

