using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class SpawnPool : General
{
    //List<GameObject> _list = new List<GameObject>();
    //List<Bullet> _whiteBullet = new List<Bullet>();
    //List<Bullet> _redBullet = new List<Bullet>();
    //List<Crate> _energyCrate = new List<Crate>();

    General[] _gen;

    void Start()
    {
        var all = GetAllChilds(gameObject);
        _gen = new General[all.Count];

        for (int i = 0; i < all.Count; i++)
        {
            _gen[i] = (all[i].GetComponent<General>());
            _gen[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {

    }

    public void AddToPool<T>(T test)
    {
        for (int i = 0; i < _gen.Length; i++)
        {
            if (_gen[i] == null)
            {
                _gen[i] = test as General;
                _gen[i].gameObject.transform.position = new Vector3();
                _gen[i].gameObject.SetActive(false);

                return;
            }
        }
    }

    public General ReturnGeneral(string which)
    {
        for (int i = 0; i < _gen.Length; i++)
        {
            if (_gen[i] != null && _gen[i].name.Contains(which))
            {
                _gen[i].gameObject.SetActive(true);
                var a = _gen[i];
                _gen[i] = null;
                return a;
            }
        }
        return null;
    }

    //public T ReturnObject<T>() where T : new()
    //{
    //    for (int i = 0; i < _gen.Count; i++)
    //    {
    //        if ((_gen[i]) == typeof(T))
    //        {
    //            return (T)_gen[i];
    //        }
    //    }
    //    return (T)null;
    //}



    //public static T GetInHeaderProperty<T>() where T : new()
    //{
    //    dynamic result = new T();
    //    result.CompanyId = "Test";
    //    result.UserId = "Test";
    //    result.Password = "Test";
    //    result.MessageId = " ";
    //    return (T)result;
    //}
}

