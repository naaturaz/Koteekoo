using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour {

    NavMeshAgent _agent;

	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();

        StartCoroutine("FourSec");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator FourSec()
    {
        while (true)
        {
            yield return new WaitForSeconds(4); // wait
            _agent.SetDestination(Program.GameScene.Player.transform.position + new Vector3(0.9f,0,0));
        }
    }
}
