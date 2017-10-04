using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Building : Shooter
{
    bool _wasFixed;
    string _root;
    string _name;

    float _prodRate = 10f;
    float _lastProd;

    General _crate;
    string _inputCrate;
    bool _hasInput;

    bool _hasEnergy = true;
    float _energyLast = 30f;
    float _energyStart;


    //Rotating childs
    public AutoMoveAndRotate _rotator1;
    public AutoMoveAndRotate _rotator2;

    float _startTime;

    static Dictionary<string, int> _indexer = new Dictionary<string, int>()
    {
        { "Defend_Tower", 0 },
        { "Solar_Panel", 1 },
        { "Small_Wall", 2 },
        { "Med_Wall", 3 },
        { "Tall_Wall", 4 },
        { "Hi_Defend_Tower", 5 },

    };

    static List<BuildStat> _builds = new List<BuildStat>()
    {
        new BuildStat("Defend_Tower", 150, 12),
        new BuildStat("Solar_Panel", 50, 10),
        new BuildStat("Small_Wall", 5, 20),
        new BuildStat("Med_Wall", 10, 25),
        new BuildStat("Tall_Wall", 15, 30),
        new BuildStat("Hi_Defend_Tower", 300, 18),

    };

    JoyStickManager _joyStickManager;


    public string Root
    {
        get
        {
            return _root;
        }

        set
        {
            _root = value;
        }
    }

    /// <summary>
    /// The name of this 
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public General Crate
    {
        get
        {
            return _crate;
        }

        set
        {
            _crate = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        _startTime = Time.time;
        _joyStickManager = FindObjectOfType<JoyStickManager>();

        IsGood = true;

        if (name.Contains("Solar") && Program.GameScene.TutoWindow != null)
        {
            Program.GameScene.TutoWindow.Next("Tuto.Solar");
        }

        if (name == "Rocket")
        {
            _wasFixed = true;
            Health = 50;
            return;
        }

        NameMeAndHealth();

        if (IsAInputBuilding(name))
        {
            _inputCrate = "Miner_Crate";
        }

        ChangeRotatorsToState(_hasEnergy);
    }

    void NameMeAndHealth()
    {
        //object that was not spawn by player
        //so far can be:
        //-Enemy_Tower on the enamy spawn point 
        if (_root == null)
        {
            Health = 100;
            return;
        }

        var arr = _root.Split('/');
        Name = arr[arr.Length - 1];

        var index = _indexer[_name];
        Health = _builds[index].Health;
    }

    static Vector3 _lastOneOfSameTypeWasPlaced;
    static Quaternion _lastPlacedRot;
    // Update is called once per frame
    void Update()
    {
        if (!_wasFixed && transform.parent != Program.GameScene.Player.transform)
        {
            if (!_joyStickManager.JoyStickController)
            {
                transform.position = Program.GameScene.Player.HitMouseOnTerrain.point;
            }
            else
            {
                transform.position = Program.GameScene.Player.transform.position + new Vector3(0, 0, 2);
            }

            //so it keeps the last pos and rotation of the last object spawned. Must have been of the same type
            if (_lastOneOfSameTypeWasPlaced != new Vector3())
            {
                transform.position = _lastOneOfSameTypeWasPlaced;
                transform.rotation = _lastPlacedRot;
            }
            //so after this if player rotates the building not placed will too
            transform.SetParent(Program.GameScene.Player.transform);
        }

        if (!_wasFixed && Time.time > _startTime + 0.3f &&
            (Input.GetMouseButtonUp(0) || _joyStickManager.ActionButtonNow()))
        {
            _joyStickManager.DonePlacing();
            if (name.Contains("Solar") && Program.GameScene.TutoWindow != null)
            {
                Program.GameScene.TutoWindow.Next("Tuto.SetBuild");
            }

            _wasFixed = true;
            Program.GameScene.BuildingManager.AddToAll(this);
            OwnTower();
            RemoveCost();
            Program.GameScene.SoundManager.PlaySound(3);
            transform.SetParent(Program.GameScene.BuildingManager.transform);

            if ((Input.GetKey(KeyCode.LeftShift) || Program.GameScene.JoyStickManager.JoyStickController)
                && DoWeHavePowerToBuildThis(_name) &&
                !Program.GameScene.EnemyManager.ThereIsAnAttackNow())
            {
                //bz sometimes the obhject will go below in Y if collided with something. 
                var yCorrected = new Vector3(transform.position.x, Program.GameScene.Player.transform.position.y,
                    transform.position.z);

                _lastOneOfSameTypeWasPlaced = yCorrected;
                transform.position = yCorrected;

                _lastPlacedRot = transform.rotation;
                Program.GameScene.BuildingManager.Create(name);
            }
            //if not power tht old pos has to be resseted bz the player will move ard 
            if (!DoWeHavePowerToBuildThis(_name))
            {
                _lastOneOfSameTypeWasPlaced = new Vector3();
            }
        }

        //Cancelling 'B' btn
        if (!_wasFixed && Program.GameScene.JoyStickManager.JoyStickController && Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            if (Program.GameScene.TutoWindow != null)
            {
                if (name.Contains("Solar"))
                {
                    Program.GameScene.TutoWindow.Next("Tuto.CancelSolar");
                }
                if (name.Contains("Small_Wall"))
                {
                    Program.GameScene.TutoWindow.Next("Tuto.Cancel.SmallWall");
                }
            }

            Program.GameScene.SoundManager.PlaySound(3);
            Program.GameScene.JoyStickManager.DonePlacing();
            Destroy(gameObject);
            _lastOneOfSameTypeWasPlaced = new Vector3();
        }
        Production();
    }

    public bool WasFixed()
    {
        return _wasFixed;
    }

    public void SetWasFixed(bool a)
    {
        _wasFixed = a;
    }

    private void RemoveCost()
    {
        var index = _indexer[_name];
        Program.GameScene.Player.Power -= _builds[index].Cost;
        Program.GameScene.BuildingManager.AddToSpent(_builds[index].Cost);
    }

    void ChangeRotatorsToState(bool isOn)
    {
        if (_rotator1 != null)
        {
            _rotator1.enabled = isOn;
        }
        if (_rotator2 != null)
        {
            _rotator2.enabled = isOn;
        }
    }


    private void Production()
    {
        if (name.Contains("Solar"))
        {
            var a = 1;
        }

        if (!_hasEnergy)
        {
            return;
        }

        var goodInput = _hasInput || !IsAInputBuilding(name);

        if (!_wasFixed || Time.time < _lastProd + _prodRate || !goodInput)
        {
            return;
        }

        var isProd = IsAProductionBuilding(name);
        if (isProd)
        {
            Produce();
        }
    }

    public static bool IsAProductionBuilding(string transfName)
    {
        return transfName.Contains("Bullets_Factory") || transfName.Contains("Parts_Factory")
            || transfName.Contains("Miner")
            || transfName.Contains("Solar_Panel");
    }

    public static bool IsAInputBuilding(string transfName)//
    {


        return transfName.Contains("Bullets_Factory") || transfName.Contains("Parts_Factory") ||
            transfName.Contains("Rocket_Base");
    }

    private void Produce()
    {
        //if time paused wont produce ,,, in Tutorial will
        if (Program.GameScene.JoyStickManager.IsTimePausedAndNotTutorial())
        {
            _lastProd = Time.time;
            return;
        }

        var root = "Prefab/Crate/" + Name + "_Crate";
        _lastProd = Time.time;
        _hasInput = false;
        //Crate = General.Create(root, transform.position + new Vector3(0, 4, 0f), root);

        Crate = Program.GameScene.SpawnPool.ReturnGeneral("Solar_Panel_Crate");
        Crate.transform.position = transform.position + new Vector3(0, 4, 0);

    }

    internal static Building CreateB(string root, Vector3 point, string buildingPath, Transform transform)
    {
        var obj = (Building)Create(root, point, buildingPath, transform);
        obj.Root = root;
        return obj;
    }

    void OwnTower()
    {
        if (name.Contains("Own_Tower"))
        {
            var cellDroppedIn = Program.GameScene.Player.HitMouseOnTerrain.collider.gameObject.GetComponent<CellGO>();

            //building second tower on same cell 
            if (cellDroppedIn == null)
            {
                return;
            }

            cellDroppedIn.ClaimedTerritory();
        }
    }


    #region Energy

    public bool HasEnergy()
    {
        return _hasEnergy;
    }

    void AddEnergy()
    {
        _hasEnergy = true;
        _energyStart = Time.time;
        ChangeRotatorsToState(true);

    }


    #endregion


    internal void AddPerson(EnemyGO enemyGO)
    {
        Debug.Log("p added");
        enemyGO.transform.parent = transform;
        enemyGO.transform.position = transform.position;

        Program.GameScene.Player.EmptyHands();
    }

    internal bool IsThisInputYouNeed(General onHands)
    {
        //solar panel doesnt need input 
        if (name.Contains("Solar_Panel"))
        {
            return false;
        }

        if (name.Contains("Rocket_Base"))
        {
            return true;
        }

        //carrying a soldier 
        if (onHands == null)
        {
            return false;
        }

        if (onHands.name.Contains("Solar_Panel_Crate"))
        {
            return true;
        }
        return onHands.name.Contains(_inputCrate);
    }

    internal void AddInput(General onHands)
    {
        if (onHands.name.Contains("Solar_Panel_Crate"))
        {
            AddEnergy();
            return;
        }

        _hasInput = true;
    }

    internal void Load(BuildingData data)
    {
        transform.position = data.Position;
        _wasFixed = true;
    }

    internal bool HasProductionOnStock()
    {
        return Crate != null;
    }

    internal void ClearBuildProd()
    {
        Crate = null;
    }




    public static bool DoWeHavePowerToBuildThis(string build)
    {
        var index = _indexer[build];
        var cost = _builds[index].Cost;

        return Program.GameScene.Player.Power >= cost;
    }

    public static BuildStat ReturnBuildStat(string build)
    {
        var index = _indexer[build];
        return _builds[index];

    }

    public static int ReturnBuildIndex(string build)
    {
        return _indexer[build];
    }


    private void OnTriggerEnter(Collider other)
    {
        BeingHit(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    void BeingHit(GameObject go)
    {
        if (!go.name.Contains("Bullet"))
        {
            return;
        }

        var bulletComponent = go.GetComponent<Bullet>();
        //so friendly fire doesnt affect units  
        if (IsGood == bulletComponent.IsGood)
        {
            return;
        }

        if (Health > 1)
        {
            Health--;
        }
        else
        {
            if (Health == 1)
            {
                if (name == "Rocket")
                {
                    Program.GameScene.Player.GameOver("Rocket booom!");
                    return;
                }

                Program.GameScene.SoundManager.PlaySound(2);

                Health = 0;
                Destroy(gameObject);
                //spawn explosion 
                var exp = General.Create("Prefab/Particles/Explosion", transform.position, "Explo");
            }
        }
    }

}


public class BuildStat
{
    public string Key;
    public int Cost;
    public int Health;

    public BuildStat(string key, int cost, int health)
    {
        Key = key;
        Cost = cost;
        Health = health;
    }

}


/// <summary>
/// For SaveLoad XML
/// </summary>
public class BuildingData
{
    public Vector3 Position;
    public string Root;
    public string Name;

    public BuildingData() { }

    public BuildingData(Vector3 position, string root)
    {
        Position = position;
        Root = root;
    }
}