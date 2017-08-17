using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

//to be able to serializea an obj cant inherit from monobeaviour

//Tutorial:
//http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer

public class XMLSerie
{
    //public static string dataPath = @"C:\Temp";
    public static string dataPath = @"F:\Temp";

    //public static string dataPath = Application.dataPath;




    private static void DefinePath()
    {
        //dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\Koteekoo";
        //Debug.Log(dataPath);
    }

    private static void CheckIfSugarMillFolderExists()
    {
        var exist = Directory.Exists(dataPath);

        if (!exist)
        {
            Directory.CreateDirectory(dataPath);
        }
    }



    public static void WriteXML(Data data, string slot)
    {
        DefinePath();
        CheckIfSugarMillFolderExists();

        DataContainer DataCollection = new DataContainer();
        DataCollection.Data1 = data;

        DataCollection.Save(Path.Combine(dataPath, "Data"+ slot+".xml"));
    }

    public static Data ReadXML(string slot)
    {
        DefinePath();

        var SaveInfoRTSCollection =
            DataContainer.Load(Path.Combine(dataPath, "Data" + slot + ".xml"));

        if (SaveInfoRTSCollection == null)
        {
            //no file saved
            return null;
        }

        Data res = SaveInfoRTSCollection.Data1;
        return res;
    }
}
