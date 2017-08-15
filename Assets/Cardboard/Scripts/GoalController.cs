using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {

	// Use this for initialization
	private GameObject player;
	private bool isColliding;
	private Orchestrator orchestrator;
	void Start () {
		player = GameObject.Find ("Player");
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		isColliding = false;
	}

	void Update () {
		isColliding = false;
	}

	void OnTriggerEnter(Collider other) {
		if(other.name == "Player" && !isColliding) {
			orchestrator.WonLevel ();
		}
		isColliding = true;
	}
}
