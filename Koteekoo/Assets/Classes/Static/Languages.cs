using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Languages
{
    private static string _currentLang = "English";


    static Dictionary<string, string> _english = new Dictionary<string, string>()
    {
      {"Tuto.Objective","The objective of the game is to ​resist as many enemy waves ​as ​you can and advance ​to the next level." },
        {"Tuto.Move","The player moves using the 'Left Stick'. Now go to the green circle with the player" },
        {"Tuto.Rotate","The player rotates using the 'Right Stick'" },
        {"Tuto.Shoot","The player shoots with 'A' button. While ​i​n Defend mode the player will shoot automatically" },

        {"Tuto.Build","Pressing 'LB'​ will bring up the building menu" },
        {"Tuto.Solar","Now​,​ in the building menu with the 'Left Stick' select 'Solar panel'. Highlight the button and then press 'A'. This building will provide you with power over time. Notice the cost and health of each unit" },

        {"Tuto.Place","Now to place the building move the player and place the building in the green circle" },
        {"Tuto.SetBuild","Set building with 'A'" },

        {"Tuto.CancelSolar","That solar building was set. You can move around see that a new one was spawn. Please cancel this one, to cancel: 'B'" },

        { "Tuto.Power","You need the power to build the buildings" },
        { "Tuto.Health","This is your health" },

        { "Tuto.Enemy","The enemies will try to kill you and destroy your rocket. If the rocket gets destroy​ed the is Game Over.​ In each level enemies select a random spawn location, but the subsequent waves in the ​same ​level will spawn ​in the same location" },

        {"Tuto.Turret","Turrets will actively protect you and shoot enemies. ​Be aware they can​not shoot through walls. Also when a new enemy wave ​is coming the turret will face directly towards the enemies​,​ so you know the direction ​from which they are coming from " },
        { "Tuto.Wall","Walls: you can place them ​strategically to protect your rocket" },
        {"Tuto.WallArdRocket","Try to create a wall around the rocket. Use only Small Wall units. Put 10 of them around the rocket. Press 'LB' to show the building menu. Use the ones on screen for reference" },
        {"Tuto.Cancel.SmallWall","Cancel the Small Wall unit with 'B'" },


        { "Tuto.CreatePath","You can build paths with turrets and use ​them to  ambush enemies. The one shown on screen is one of infinite possiblilites" },

        {"Tuto.NextWave","This is the remaining time until next ​enemy ​wave. Outside of the tutorial the time won't pass if you are in building mode" },
        {"Tuto.Time","This is the remaining time until you pass this level"},
        {"Tuto.Jump","To jump press 'X' button"},


        { "Tuto.Tuto","Be creative ​in defending your rocket. Place turrets behinds walls. Have fun!!"},




        //units description
        { "Defend_Tower","Shoots enemies"},
        { "Hi_Defend_Tower","Shoots enemies from ​a ​higher point"},
        { "Solar_Panel","Produces power over time"},
        { "Small_Wall","​Minimal Protection"},
        { "Med_Wall","​Medium Protection"},
        { "Tall_Wall","​Ultimate Protection"},

        //name
                { "Defend_Tower.Unit.Name","Tower"},
        { "Hi_Defend_Tower.Unit.Name","High Tower"},
        { "Solar_Panel.Unit.Name","Solar Panel"},
        { "Small_Wall.Unit.Name","Small Wall"},
        { "Med_Wall.Unit.Name","Medium Wall"},
        { "Tall_Wall.Unit.Name","Tall Wall"},
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
