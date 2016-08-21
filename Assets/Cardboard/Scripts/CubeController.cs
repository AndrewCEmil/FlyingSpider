using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CubeController : MonoBehaviour, ICardboardGazeResponder {
	public GameObject player;

	private PlayerController playerController;
	private bool isLinked;
	private bool isGazedAt;

	void Start() {
		SetGazedAt(false);
		SetLinked (false);
		playerController = player.GetComponent<PlayerController> ();
	}

	public void Update() {
	}

	public void SetGazedAt(bool gazedAt) {
		if (!isLinked && isGazedAt != gazedAt) {
			if (gazedAt) {
				makeGreen ();
			} else {
				makeRed ();
			}
		}
		isGazedAt = gazedAt;
	}

	public void makeGreen() {
		GetComponent<Renderer> ().material.color = Color.green;
	}

	public void makeRed() {
		GetComponent<Renderer> ().material.color = Color.red;
	}

	public void makeBlue() {
		GetComponent<Renderer> ().material.color = Color.blue;
	}

	public void SetLinked(bool linked) {
		if(isLinked != linked) {
			if (linked) {
				makeBlue ();
			} else if (isGazedAt) {
				makeGreen ();
			} else {
				makeRed();
			}
			isLinked = linked;
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
		SetLinked (playerController.Link(gameObject));
	}

	private void destroyLink() {
		playerController.Unlink ();
	}
}
