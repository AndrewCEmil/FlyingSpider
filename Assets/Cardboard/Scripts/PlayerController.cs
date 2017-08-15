using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public ParticleSystem beam;
	private SpriteController winSpriteController;
	private GameObject linkedTarget;
	private Rigidbody rb;
	private Orchestrator orchestrator;
	void Start () {
		linkedTarget = null;
		rb = GetComponent<Rigidbody> ();
		Physics.gravity = new Vector3(0, -0.2F, 0);
		Physics.bounceThreshold = 0;
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}

	public void Link(GameObject target) {
		linkedTarget = target;
		beam.maxParticles = 100;
	}

	public void Unlink() {
		linkedTarget = null;
		beam.maxParticles = 0;
	}


	void Update () {
		if (linkedTarget != null) {
			Vector3 velocityAdd = (linkedTarget.transform.position - transform.position).normalized / 50;
			rb.velocity += velocityAdd;
			PointParticles (linkedTarget);
		} else {
			MaybeReset ();
		}
	}

	private void PointParticles(GameObject target) {
		float distance = Vector3.Distance (transform.position, target.transform.position) - 1f;
		beam.startLifetime = distance / beam.startSpeed;
		beam.transform.LookAt (target.transform.position);
	}

	private void MaybeReset() {
		if (Vector3.Distance (transform.position, new Vector3 (0, 0, 0)) > 150) {
			orchestrator.ResetPlayer ();
		}
	}
}
