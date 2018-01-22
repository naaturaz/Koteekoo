using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoStepGO : General
{
    //Assign on inpsector
    public string AccomplishedThisEvent;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && Program.GameScene.TutoWindow.IsCurrentStep("Tuto.Move"))
        {
            StepReached();
        }
        if (other.name.Contains("Solar") && Program.GameScene.TutoWindow.IsCurrentStep("Tuto.Place"))
        {
            StepReached();
        }
        if (other.name.Contains("Obstacle_3") && Program.GameScene.TutoWindow.IsCurrentStep("Tuto.Move.Block"))
        {
            StepReached();
        }
    }

    void StepReached()
    {
        Program.GameScene.TutoWindow.Next(AccomplishedThisEvent);
        gameObject.SetActive(false);
    }
}
