using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class SphereController : MonoBehaviour, ICardboardGazeResponder {
	public GameObject player;

	private bool isLinked;
	private bool isGazedAt;
	private ParticleSystem particles;
	private ParticleSystem.ColorOverLifetimeModule col;
	private Orchestrator orchestrator;

	void Awake() {
		Initialize ();
	}

	public void Update() {
	}

	public void Initialize() {
		particles = GetComponentInChildren<ParticleSystem> ();
		col = particles.colorOverLifetime;
		SetGazedAt(false);
		SetLinked (false);
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}

	public void SetGazedAt(bool gazedAt) {
		isGazedAt = gazedAt;
		if (!isLinked) {
			if (gazedAt) {
				makeGazedColor ();
			} else {
				makeUngazedColor ();
			}
		}
	}

	//TODO change function names
	//Gazed on
	public void makeGazedColor() {
		Color targetColor = Color.green;
		//Color targetColor = new Color (.914f, .00f, .310f);
		GetComponent<Renderer> ().material.color = targetColor;
		col.color = new ParticleSystem.MinMaxGradient (targetColor, Color.white);
	}

	//Nothing
	public void makeUngazedColor() {
		Color targetColor = Color.red;
		//Color targetColor = new Color (.094f, .432f, .675f);
		GetComponent<Renderer> ().material.color = targetColor;
		col.color = new ParticleSystem.MinMaxGradient (targetColor, Color.white);
	}

	//Selected
	public void makeLinkedColor() {
		Color targetColor = Color.blue;
		//Color targetColor = new Color (.169f, .813f, .650f);
		GetComponent<Renderer> ().material.color = targetColor;
		col.color = new ParticleSystem.MinMaxGradient (targetColor, Color.white);
	}

	public void SetLinked(bool linked) {
		isLinked = linked;
		if (linked) {
			makeLinkedColor ();
		} else if (isGazedAt) {
			makeGazedColor ();
		} else {
			makeUngazedColor();
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
		orchestrator.Link (gameObject);
	}

	#endregion
}
