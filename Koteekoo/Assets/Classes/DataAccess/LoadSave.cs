using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave
{
    static Data _data;

    public static bool ThereIsALoad()
    {
        return _data != null;
    }

    public static void LoadNow()
    {
        _data.Load();
    }

    public static void Load(string numb)
    {
        _data = XMLSerie.ReadXML(numb);
    }

    public static void Save(string numb)
    {
        Data data = new Data(true);
        XMLSerie.WriteXML(data, numb);
    }

}
