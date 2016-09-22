using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CubeController : MonoBehaviour, ICardboardGazeResponder {
	public GameObject player;

	private PlayerController playerController;
	private bool isLinked;
	private bool isGazedAt;
	private ParticleSystem particles;
	private ParticleSystem.ColorOverLifetimeModule col;

	void Awake() {
		Initialize ();
	}

	public void Update() {
	}

	public void Initialize() {
		particles = GetComponentInChildren<ParticleSystem> ();
		col = particles.colorOverLifetime;
		playerController = player.GetComponent<PlayerController> ();
		SetGazedAt(false);
		SetLinked (false);
	}

	public void SetGazedAt(bool gazedAt) {
		isGazedAt = gazedAt;
		if (!isLinked) {
			if (gazedAt) {
				makeGreen ();
			} else {
				makeRed ();
			}
		}
	}

	public void makeGreen() {
		GetComponent<Renderer> ().material.color = Color.green;
		col.color = new ParticleSystem.MinMaxGradient (Color.green, Color.white);
	}

	public void makeRed() {
		GetComponent<Renderer> ().material.color = Color.red;
		col.color = new ParticleSystem.MinMaxGradient (Color.red, Color.white);
	}

	public void makeBlue() {
		GetComponent<Renderer> ().material.color = Color.blue;
		col.color = new ParticleSystem.MinMaxGradient (Color.blue, Color.white);
	}

	public void SetLinked(bool linked) {
		isLinked = linked;
		if (linked) {
			makeBlue ();
		} else if (isGazedAt) {
			makeGreen ();
		} else {
			makeRed();
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
		playerController.NewTarget(gameObject);
	}

	#endregion
}
