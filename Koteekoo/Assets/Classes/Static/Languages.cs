using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Languages
{
    private static string _currentLang = "English";


    static Dictionary<string, string> _english = new Dictionary<string, string>()
    {
        //Tutorial
        {"Tuto.Objective","The objective of the game is to ​resist all enemy waves" },
        {"Tuto.Move","The player moves using the 'Left Stick'. Now go to the green circle with the player" },
        {"Tuto.Rotate","The player rotates using the 'Right Stick'" },



        { "Tuto.Shoot","When an attack is happening the player will shoot automatically" },

        {"Tuto.Build","Pressing 'LB'​ will bring up the building menu" },
        {"Tuto.Solar","Now​,​ in the building menu with the 'Left Stick' select 'Solar Panel'. Highlight the button and then press 'A'. This building will provide you with power over time. Notice the cost and health of each unit" },

        {"Tuto.Place","To place the Solar Panel move the player to the green circle. The Solar Panel will rotate along with the player" },
        {"Tuto.SetBuild","Set the Solar Panel with 'A'" },

        {"Tuto.CancelSolar","That Solar Panel was placed. By moving around you can see that another was spawned. To cancel this one press 'B'" },

        { "Tuto.Power","You need power to build buildings" },
        { "Tuto.Health","This is your health" },

        { "Tuto.Enemy","The enemies will try to kill you and destroy your rocket. If the rocket gets destroy​ed Game is Over​" },

        {"Tuto.Turret","Turrets will actively protect you and shoot enemies. They can​not shoot through walls" },
        { "Tuto.Wall","Walls: you can ​strategically place them to protect your rocket" },
        {"Tuto.WallArdRocket","Try to create a wall around the rocket. Use only 'Small Wall' units. Put 10 of them around the rocket. Press 'LB' to show the building menu. Use the ones on screen for reference" +
            "\n\nUse the 'Right Stick' to rotate the building"},
        {"Tuto.Cancel.SmallWall","To cancel the 'Small Wall' press 'B'" },


        { "Tuto.CreatePath","You can build paths with turrets and use ​them to ambush enemies" },

        {"Tuto.NextWave","This is the remaining time until next ​enemy ​wave. Outside of the tutorial the time won't pass if you are in building mode" },
        {"Tuto.Waves","This is the remaining amount of enemies waves to pass this level"},
        {"Tuto.Jump","To jump press 'X' button"},

        { "Tuto.Restart","You can restart the game at any time by pressing 'Menu' and then selecting 'Restart'"},
        {"Tuto.BigArrow","The purple arrows pointing to the rocket shows where the enemy wave will come from" },

        {"Tuto.Move.Block","Follow the yellow arrow and push the Red Cube into the circle" },

        { "Tuto.Tip","Waves get harder in each level"},
        { "Tuto.EnemyTip","As you damage enemies, they will drop energy, loot it!"},

        { "Tuto.Tuto","Be creative ​in defending your rocket. Place turrets behinds walls. Have fun!! Let's see how far can you get!"},




        //units description
        { "Defend_Tower","Shoots enemies"},
        { "Hi_Defend_Tower","Shoots enemies from ​a ​higher point"},
        { "Solar_Panel","Produces power over time"},
        { "Small_Wall","​Minimal Protection"},
        { "Med_Wall","​Medium Protection"},
        { "Tall_Wall","​Ultimate Protection"},
        { "Pit","Pit"},

        //name
        { "Defend_Tower.Unit.Name","Tower"},
        { "Hi_Defend_Tower.Unit.Name","High Tower"},
        { "Solar_Panel.Unit.Name","Solar Panel"},
        { "Small_Wall.Unit.Name","Small Wall"},
        { "Med_Wall.Unit.Name","Medium Wall"},
        { "Tall_Wall.Unit.Name","Tall Wall"},
        { "Pit.Unit.Name","Pit"},

    };

    static string[] EnglishQuotes = new string[] {
        "You are awesome",
        "Reasons why I think you are awesome: #1, You are",
        "Live in the present",
        "Remember you are awesome, beautiful, kind & smart",
        "Be here now",
        "Keep it up",
        "Just be happy",
        "Heal the past, live the present, dream the future",
        "You is kind!",
        "You is smart!",
        "You is important!",
        "Life is tough, but so are you",
        "You are unique!",
        "You are beautiful",
        "Don't worry be happy",
        "Now is the time",
        "Don't forget to smile",
        "There is always a reason to smile",
        "Be happy",
        "Be bright",
        "Be you",
        "Smile often",
        "Always be happy",
        "The art of life is to live in the present moment",
        "Live now",
        "Here & now",
        "Be present",
        "Think happy, be happy",
        "Smile",
        "Remember to be happy",
        "Be happy and smile",
        "Keep on smiling",
        "Smile again",
        "Just smile",
        "Keep smiling"
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

    public static string ReturnAQuote(int i)
    {
        var rand = new System.Random();

        if (i < 0 || i > EnglishQuotes.Length-1)
            return EnglishQuotes[rand.Next(0, EnglishQuotes.Length)];

        return EnglishQuotes[i];
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
