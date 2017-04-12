using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour {

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
    void Start ()
    {

        _gameScene = new GameScene();

        var empty = (GameObject)Resources.Load("Main/EmptyGO");
        _gameScene.Container = Instantiate(empty, empty.transform.position, empty.transform.rotation);
        _gameScene.Container.name = "GameScene";

        _gameScene.Start();
    }
	
	// Update is called once per frame
	void Update () {

    }
}
