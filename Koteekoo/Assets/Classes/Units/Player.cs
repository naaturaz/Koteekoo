using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Player : Shooter
{
    public Image GotHit1;
    public Image GotHit2;
    public Image GotHit3;

    public GameObject Geometry;

    float _speed = 5;//.05    .1
    RaycastHit _hitMouseOnTerrain;
    Rigidbody _rigidBody;
    bool _isFalling;
    bool _hasHandOccupied;
    General _onHands;

    JoyStickManager _joyStickManager;

    private int _score;

   

    public bool IsMouseOnTerrain { get; private set; }

    public RaycastHit HitMouseOnTerrain
    {
        get
        {
            return _hitMouseOnTerrain;
        }

        set
        {
            _hitMouseOnTerrain = value;
        }
    }

    public bool IsFalling
    {
        get
        {
            return _isFalling;
        }

        set
        {
            _isFalling = value;
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
        }
    }



    // Use this for initialization
    void Start()
    {
        GotHit1.color = new Color(0, 0, 0, 0);
        GotHit2.color = new Color(0, 0, 0, 0);
        GotHit3.color = new Color(0, 0, 0, 0);

        IsGood = true;

        _rigidBody = GetComponent<Rigidbody>();
        base.Start();
        Health = 10;
        Ammo = 2000;

        _joyStickManager = FindObjectOfType<JoyStickManager>();


        base.CreateHealthBar();

        StartCoroutine("HalfASec");
    }

    internal void AddPower(int v)
    {
        Program.GameScene.BuildingManager.AddToGen(v);
        Power += v;
    }

    private IEnumerator HalfASec()
    {
        while (true)
        {
            yield return new WaitForSeconds(.15f); // wait
            CheckIfHitLessThan3SecAgo();
        }
    }

    internal void Add1Score()
    {
        Program.GameScene.SoundManager.PlaySound(11);
        Score++;

        AddPower(2);
    }

    internal void Add1Life()
    {
        Health++;
        Program.GameScene.SoundManager.PlaySound(12, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath())
        {
            GameOver("The Player got Killed! ");
            return;
        }

        //UpdateHitMouseOnTerrain();
        //LookAtMousePos();

        Movement();

        if (!Program.GameScene.BuildingManager.IsBuildingNow())
        {
            Shoot();
        }

        Jump();
        CheckCeiling();

        UnableRigidIfBuilding();

        CheckIfNeedsToFadeOut();
    }

    //private void FixedUpdate()
    //{
    //    Movement();
    //}

    private void CheckCeiling()
    {
        if (transform.position.y > 5f)
        {
            _rigidBody.AddForce(new Vector3(0, -1f, 0), ForceMode.VelocityChange);
        }
    }

    public void GameOver(string reason)
    {
        Analytics.CustomEvent("GameOver", new Dictionary<string, object> { { "reason", reason }, });

        Debug.Log("Game Over");
        Application.LoadLevel("MainMenu");
        PlayerPrefs.SetString("State", "GameOver");//Clear current game 
        PlayerPrefs.SetString("Reason", reason);
    }

    private void UnableRigidIfBuilding()
    {
        return;

        if (Program.GameScene.BuildingManager.IsBuildingNow())
        {
            _rigidBody.isKinematic = true;
        }
        else
        {
            _rigidBody.isKinematic = false;
        }
    }

    private void Jump()
    {
        if ((Input.GetKeyDown("space"))
            || (_joyStickManager.JoyStickController && Input.GetKeyUp(KeyCode.Joystick1Button2))
            && !IsFalling)
        {
            Analytics.CustomEvent("Player.Jump", new Dictionary<string, object> { { "Jump Now", "" }, });


            _rigidBody.AddForce(new Vector3(0, 7, 0), ForceMode.VelocityChange);
        }
        IsFalling = true;
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        //we are on something
        IsFalling = false;
    }


    void Movement()
    {
        if (_joyStickManager.ShouldStopPlayerMovement())
        {
            return;
        }

        float v = Input.GetAxis("Vertical") * .1f;
        float h = Input.GetAxis("Horizontal") * .1f;

        //float v = Input.GetAxis("Vertical") * _speed *  Mathf.Abs(Time.deltaTime);
        //float h = Input.GetAxis("Horizontal") * _speed * Mathf.Abs(Time.deltaTime);
        transform.position += new Vector3(h, 0, v);
    }

    private void LookAtMousePos()
    {
        if (_joyStickManager.JoyStickController)
        {
            return;
        }

        transform.LookAt(HitMouseOnTerrain.point);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    public void UpdateHitMouseOnTerrain()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        // This would cast rays only against colliders in layer 8.
        int layerMask = 1 << 8;
        // Does the ray intersect any objects in the layer 8 "Terrain Layer"
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hitMouseOnTerrain,
            Mathf.Infinity, layerMask))
        {
            IsMouseOnTerrain = true;
        }
        else
        {
            //Debug.Log("Mouse Did not Hit Layer 8: Terrain");
            IsMouseOnTerrain = false;
        }

        //Debug.Log(HitMouseOnTerrain.collider.gameObject.name);
    }


    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        //Debug.Log("OnTriggerEnter: > "+ other.gameObject.name);
        if ((other.gameObject.name.Contains("Prefab/Terrain") || other.gameObject.name.Contains("Cube")
            || other.gameObject.name.Contains("Militar"))
            && _isFalling)
        {
            IsFalling = false;
        }
    }



    #region Pick Up

    void OnCollisionEnter(Collision collision)
    {
        if (_hasHandOccupied)
        {
            //HandleTouchInputBuilding(collision);


            return;
        }

        //HandleTouchEnemy(collision);
        //HandleTouchProdBuilding(collision);
        //HandleTouchUnit(collision);
    }

    private void HandleTouchUnit(Collision collision)
    {
        //if (Unit.IsAnUnit(collision.gameObject.name))
        //{
        //    if (Ammo > 200)
        //    {

        //    }
        //}
    }

    private void HandleTouchInputBuilding(Collision collision)
    {
        if (Building.IsAInputBuilding(collision.gameObject.name) ||
            (_onHands != null && _onHands.name.Contains("Solar_Panel_Crate")))
        {
            var build = collision.gameObject.GetComponent<Building>();
            if (build == null)
            {
                return;
            }

            if (build.IsThisInputYouNeed(_onHands))
            {
                build.AddInput(_onHands);
                Destroy(_onHands.gameObject);
                _hasHandOccupied = false;
            }
        }
    }

    private void HandleTouchProdBuilding(Collision collision)
    {
        if (Building.IsAProductionBuilding(collision.gameObject.name))
        {
            var build = collision.gameObject.GetComponent<Building>();
            if (build.HasProductionOnStock())
            {
                if (build.Crate.name.Contains("Bullet"))
                {
                    Destroy(build.Crate.gameObject);
                    build.ClearBuildProd();
                    Ammo += 100;
                    return;
                }

                build.Crate.transform.parent = BulletSpawn.transform;
                build.Crate.transform.position = BulletSpawn.transform.position;
                _hasHandOccupied = true;
                _onHands = build.Crate;
                build.ClearBuildProd();
            }
        }


    }

    internal int EnergyGenerated()
    {
        throw new NotImplementedException();
    }



    void HandleTouchEnemy(Collision collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<EnemyGO>();
            if (enemy.IsDeath())
            {
                collision.gameObject.transform.parent = BulletSpawn.transform;
                collision.gameObject.transform.position = BulletSpawn.transform.position;
                _hasHandOccupied = true;
            }
        }
    }

    internal void EmptyHands()
    {
        _hasHandOccupied = false;
    }


    #endregion

    #region Hit and Fade

    bool _isFadingOut;
    
    //if the player was hit less than 3s ago will flash and will not receive damage
    float _hitAt;


    public void Hit()
    {
        //shielding if was hit less thn 3 sec ago
        if (WasHitInTheLast3Sec())
        {
            Health++;
            return;
        }

        GotHit1.color = Color.white;
        GotHit2.color = Color.white;
        GotHit3.color = Color.white;

        Program.GameScene.SoundManager.PlaySound(8, 1.2f);
        _hitAt = Time.time;
    }

    void CheckIfHitLessThan3SecAgo()
    {
        if (WasHitInTheLast3Sec() && Geometry.gameObject.activeSelf)
        {
            Geometry.gameObject.SetActive(false);
        }
        else if(!Geometry.gameObject.activeSelf)
        {
            Geometry.gameObject.SetActive(true);
        }
    }

    bool WasHitInTheLast3Sec()
    {
        return _hitAt != 0 && _hitAt + 2 > Time.time;
    }

    void CheckIfNeedsToFadeOut()
    {
        if (GotHit1.color == Color.white)
        {
            _isFadingOut = true;
        }
        FadeOut();
    }

    void FadeOut()
    {
        if (_isFadingOut)
        {
            GotHit1.color = ReturnFadedColorBy(GotHit1.color, -.05f);
            GotHit2.color = ReturnFadedColorBy(GotHit2.color, -.008f);
            GotHit3.color = ReturnFadedColorBy(GotHit3.color, -.004f);

            if (GotHit1.color.a <= 0 && GotHit2.color.a <= 0 && GotHit3.color.a <= 0)
            {
                _isFadingOut = false;
            }
        }
    }

    Color ReturnFadedColorBy(Color c, float by)
    {
        var colTemp = c;
        colTemp.a += by;
        return colTemp;
    } 

    #endregion
}
