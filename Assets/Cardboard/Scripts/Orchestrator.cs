using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {

	public GameObject player;
	public GameObject Goal;
	private LevelController levelController;
	private GameObject linkedSphere;
	private PlayerController playerController;
	private int currentLevel;
	// Use this for initialization
	void Start () {
		levelController = GameObject.Find ("LevelObject").GetComponent<LevelController> ();
		playerController = player.GetComponent<PlayerController> ();
		linkedSphere = null;
		InitCurrentLevel ();
		LoadCurrentLevel();
	}

	private void InitCurrentLevel() {
		currentLevel = PlayerPrefs.GetInt ("CurrentLevel");
		if (currentLevel == 0) {
			currentLevel = 1;
			PlayerPrefs.SetInt ("CurrentLevel", currentLevel);
			PlayerPrefs.Save ();
		}
	}

	private void ClearLevel() {
		//Clear Cubes
		GameObject[] existingCubes = GameObject.FindGameObjectsWithTag("Well");
		foreach (GameObject existingCube in existingCubes) {
			Destroy (existingCube);
		}

		//Clear Drifts
		GameObject[] existingDrifts = GameObject.FindGameObjectsWithTag("Drift");
		foreach (GameObject existingDrift in existingDrifts) {
			Destroy (existingDrift);
		}

		//Clear player
		player.GetComponent<Rigidbody> ().velocity = new Vector3(0, 0, 0);
	}

	public void ResetPlayer() {
		//TODO alert player via flash/shake
		//damageSpriteController.Flash ();
		Handheld.Vibrate ();

		SetupLevel (currentLevel);
	}

	public void Unlink() {
		if (linkedSphere != null) {
			linkedSphere.GetComponent<SphereController> ().SetLinked (false);
		}
		linkedSphere = null;
		playerController.Unlink ();
	}

	public void Link(GameObject sphere) {
		Unlink ();
		linkedSphere = sphere;
		linkedSphere.GetComponent<SphereController> ().SetLinked (true);
		playerController.Link (sphere);
	}

	private void SetupLevel(int levelNum) {
		Level level = LevelProvider.GetLevel (levelNum);
		levelController.LoadLevel (level);
	}

	private void LoadCurrentLevel() {
		ClearLevel ();
		SetupLevel (currentLevel);
	}

	public void WonLevel() {
		//TODO alert player via flash/shake
		Handheld.Vibrate ();

		//Handle level increments
		currentLevel += 1;
		if (currentLevel > LevelProvider.GetLevels ().Length) {
			//Entire game won TODO
			currentLevel = 1;
		}
		LevelProvider.OpenLevelLock (currentLevel);
		PlayerPrefs.SetInt ("CurrentLevel", currentLevel);
		PlayerPrefs.Save ();

		LoadCurrentLevel ();
	}
}
