using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Languages
{
    private static string _currentLang = "English";


    static Dictionary<string, string> _english = new Dictionary<string, string>()
    {


        {"Tuto.Objective","The objective of the game is to subsist as many enemy waves you can and advance in level. Each level gets harder" },
        {"Tuto.Build","You can build Pressing 'LB'" },
        {"Tuto.Power","You need the power to build any building" },

        {"Tuto.Solar","Solar panels will provide you with more power over time" },

        {"Tuto.Turret","The turret will actively protect you and shoot against enemies" },
        {"Tuto.Wall","You can create anything you want with those. They protect" },
        {"Tuto.WallArdRocket","Try and create a wall around the whole rocket" },
        {"Tuto.CreatePath","Try and create a path so you ambush the enemies" },



    };



































    //ESPANNOL
    private static string _houseTailES = ". A los Azucareros les encanta comerse una buena comida de vez en cuando";
    private static string _animalFarmTailES = ", aqui se pueden criar diferentes animales";
    private static string _fieldFarmTailES = ", aqui se puede cultivar diferentes cultivos";
    private static string _asLongHasInputES = ", siempre y cuando tenga la materia prima necesaria";
    private static string _produceES = "Aqui los trabajadores produciran el producto selectionado, siempre y cuando exista la materia prima";
    private static string _storageES =
        "Aqui se almacenan todos los productos, si se llena los ciudadanos no tendran donde almacenar sus cosas";
    private static string _militarES = "Con esta construccion la Amenaza Pirata decrece, " +
                                       "para ser efectiva necesita trabajadores. Mientras mas, mejor";


    static Dictionary<string, string> _spanish = new Dictionary<string, string>()
    {

  
 
    };

    public static string ReturnString(string key)
    {
        if (_currentLang == "English")
        {
            if (_english.ContainsKey(key))
            {
                return _english[key];
            }
            //in English if key is not found will return key alone 
            //'Potato' is an ex, will passed as a key and is not even in the Dict
            return key;
        }
        else if (_currentLang == "Español(Beta)")
        {
            if (_spanish.ContainsKey(key))
            {
                return _spanish[key];
            }
            return key + " not in ES Languages";
        }
        return "not languages selected ";
    }

    public static void SetCurrentLang(string lang)
    {
        _currentLang = lang;
    }

    internal static string CurrentLang()
    {
        return _currentLang;
    }

    internal static List<string> AllLang()
    {
        return new List<string>() { "English", "Spanish" };
    }

    internal static bool DoIHaveHoverMed(string key)
    {
        var currentLang = ReturnCurrentDict();

        return currentLang.ContainsKey(key + ".HoverMed");
    }

    static Dictionary<string, string> ReturnCurrentDict()
    {
        if (_currentLang == "Español")
        {
            return _spanish;
        }

        return _english;

    }
}
