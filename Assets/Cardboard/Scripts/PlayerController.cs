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
		Physics.gravity = new Vector3(0, -0.2F, 0);
	}

	public void NewTarget(GameObject target) {
		Unlink ();
		Link (target);
	}

	//Always links or unlinks - bad code
	public void Link(GameObject target) {
		linkedTarget = target;
		linkedTarget.GetComponent<CubeController> ().SetLinked (true);
		isLinked = true;
	}

	public void Unlink() {
		if (isLinked) {
			linkedTarget.GetComponent<CubeController> ().SetLinked (false);
		}
		isLinked = false;
		linkedTarget = null;
	}

	// Update is called once per frame
	void Update () {
		if (isLinked) {
			Vector3 velocityAdd = (linkedTarget.transform.position - transform.position).normalized / 50;
			//if (velocityAdd.y > 0f) {
				//velocityAdd.y += .3f;
			//}
			rb.velocity += velocityAdd;
		}
	}
}
