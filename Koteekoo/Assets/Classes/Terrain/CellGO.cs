using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CellGO : General
{
    Cell _cell;
    GameObject _claimed;
    public Cell Cell
    {
        get
        {
            return _cell;
        }

        set
        {
            _cell = value;
        }
    }

    static public CellGO Create(string root, Vector3 origen, string name, Transform container, Cell cell)
    {
        CellGO obj = null;
        obj = (CellGO)Resources.Load(root, typeof(CellGO));
        obj = (CellGO)Instantiate(obj, origen, Quaternion.identity);
        obj.transform.name = name;

        obj.Cell = cell;

        if (container != null) { obj.transform.SetParent(container); }
        return obj;
    }

    internal void ClaimedTerritory()
    {
        _claimed.SetActive(true);
    }



    // Use this for initialization
    void Start()
    {
        _claimed = GetChildCalled("Claimed");
        _claimed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void PlayerEnter()
    {
        Cell.CreateAllSiblings();
    }
}

