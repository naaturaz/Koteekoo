using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Client;

public class Program : General
{

    static GameScene _gameScene;


    public static GameScene GameScene
    {
        get
        {
            return _gameScene;
        }

        set
        {
            _gameScene = value;
        }
    }

// Use this for initialization
void Start()
    {
        Application.targetFrameRate = 60;

        _gameScene = new GameScene();
        var empty = (GameObject)Resources.Load("Main/EmptyGO");

        _gameScene.Container = Instantiate(empty, empty.transform.position, empty.transform.rotation);
        _gameScene.Container.name = "GameScene";

        _gameScene.Start();

        if (Application.loadedLevelName == "MainMenu")
        {
            return;
        }

        StartCoroutine("OneSecUpdate");


        //var root = "Prefab/Crate/Solar_Panel_Crate";
        //var Crate = General.Create(root, transform.position, root);

    }



    private IEnumerator OneSecUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // wait
            GameScene.OneSecUpdate();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameScene != null)
        {
            GameScene.Update();
        }
    }

    /// <summary>
    /// Clears current
    /// </summary>
    private void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }
}
