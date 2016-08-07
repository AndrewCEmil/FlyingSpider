using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CubeController : MonoBehaviour, ICardboardGazeResponder {
	private Vector3 startingPosition;

	void Start() {
		startingPosition = transform.localPosition;
		SetGazedAt(false);
	}

	public void SetGazedAt(bool gazedAt) {
		GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
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
		GetComponent<Renderer> ().material.color = Color.blue;
	}

	#endregion
}
