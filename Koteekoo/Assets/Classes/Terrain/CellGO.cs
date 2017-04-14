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

    bool _wasAdded;
    float _startTime;


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
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_wasAdded && Time.time > _startTime + 1)
        {
            var aa = gameObject.GetComponent<NavMeshSourceTag>();
            aa.enabled = true;

            var aaa = gameObject.GetComponent<LocalNavMeshBuilder>();
            aaa.enabled = true;

            _wasAdded = true;
        }
    }

    internal void PlayerEnter()
    {
        Cell.CreateAllSiblings();
    }
}

