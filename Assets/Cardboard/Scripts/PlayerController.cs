using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private bool isLinked;
	private GameObject linkedTarget;
	private Rigidbody rb;
	void Start () {
		isLinked = false;
		linkedTarget = null;
		rb = GetComponent<Rigidbody> ();
	}


	//Returns true if link is created
	public bool Link(GameObject target) {
		if (!isLinked) {
			isLinked = true;
			linkedTarget = target;
			return true;
		} else {
			return false;
		}
	}

	public void Unlink() {
		isLinked = false;
		linkedTarget = null;
	}

	// Update is called once per frame
	void Update () {
		if (isLinked) {
			Vector3 velocityAdd = (linkedTarget.transform.position - transform.position).normalized / 10;
			if (velocityAdd.y > 0) {
				velocityAdd.y += .5f;
			}
			rb.velocity += velocityAdd;
		}
	}
}
