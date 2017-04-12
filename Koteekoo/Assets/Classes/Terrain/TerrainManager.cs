using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TerrainManager
{
    List<Cell> _allCells = new List<Cell>();

    public TerrainManager()
    {


 
    }

    public void Start()
    {
        Cell initCell = new Cell(new Vector2());
        Program.GameScene.TerrainManager.AddCell(initCell);

        initCell.CreateAllSiblings();
    }


    public void AddCell(Cell cell)
    {
        _allCells.Add(cell);
    }

    public bool ExistCell(Vector2 nominalPos)
    {
        var found = _allCells.Find(a => a.NominalPos == nominalPos);
        return found != null;
    }
}

