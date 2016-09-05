using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject damageSprite;
	public GameObject winSprite;
	private SpriteController damageSpriteController;
	private SpriteController winSpriteController;
	private bool isLinked;
	private GameObject linkedTarget;
	private Rigidbody rb;
	private LevelController levelController;
	void Start () {
		isLinked = false;
		linkedTarget = null;
		rb = GetComponent<Rigidbody> ();
		Physics.gravity = new Vector3(0, -0.2F, 0);
		damageSpriteController = damageSprite.GetComponent<SpriteController> ();
		winSpriteController = winSprite.GetComponent<SpriteController> ();
		GameObject levelObject = GameObject.Find ("LevelObject");
		levelController = levelObject.GetComponent<LevelController> ();
		PlayerPrefs.DeleteAll ();
		LoadNextLevel ();
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
			rb.velocity += velocityAdd;
		} else {
			if (Vector3.Distance (transform.position, new Vector3 (0, 0, 0)) > 50 || transform.position.y < 0) {
				Reset ();
			}
		}
	}

	//Used to move player back to start
	void Reset() {
		rb.velocity = new Vector3 (0, 0, 0);
		transform.position = new Vector3 (0, .5f, 0);
		damageSpriteController.Flash ();
		Handheld.Vibrate ();
		isLinked = false;
		linkedTarget = null;
	}


	//Used to clear links on new level
	public void Clear() {
		isLinked = false;
		linkedTarget = null;
	}

	public void Success() {
		//TODO score?
		winSpriteController.Flash();
		LoadNextLevel ();
	}

	void LoadNextLevel() {
		int nextLevel = PlayerPrefs.GetInt ("CurrentLevel");
		nextLevel++;
		levelController.LoadLevel (LevelProvider.GetLevel (nextLevel));
		PlayerPrefs.SetInt ("CurrentLevel", nextLevel);
		PlayerPrefs.Save ();
	}
}
