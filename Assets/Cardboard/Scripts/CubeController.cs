using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CubeController : MonoBehaviour, ICardboardGazeResponder {
	public GameObject player;

	private Rigidbody playerRb;
	private bool isLinked;
	private bool isGazedAt;

	void Start() {
		SetGazedAt(false);
		isLinked = false;
		playerRb = player.GetComponent<Rigidbody> ();
	}

	public void Update() {
		if (isLinked) {
			Vector3 velocityAdd = (transform.position - player.transform.position).normalized / 10;
			if (velocityAdd.y > 0) {
				velocityAdd.y += .5f;
			}
			playerRb.velocity += velocityAdd;
		}
	}

	public void SetGazedAt(bool gazedAt) {
		if (!isLinked) {
			GetComponent<Renderer> ().material.color = gazedAt ? Color.green : Color.red;
			isGazedAt = gazedAt;
		}
	}

	#region ICardboardGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see CardboardGaze).
	public void OnGazeEnter() {
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
	}

	// Called when the Cardboard trigger is used, between OnGazeEnter
	/// and OnGazeExit.
	public void OnGazeTrigger() {
		createLink ();
	}

	#endregion

	private void createLink() {
		isLinked = true;
		GetComponent<Renderer> ().material.color = Color.blue;
	}

	private void destroyLink() {
		isLinked = false;
		SetGazedAt (isGazedAt);
	}
}
