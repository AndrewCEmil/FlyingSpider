using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public GameObject baseCube;
	public GameObject player;
	public GameObject goal;

	public void LoadLevel(Level level) {
		//First find all existing cubes and remove them
		GameObject[] existingCubes = GameObject.FindGameObjectsWithTag("Well");
		foreach (GameObject existingCube in existingCubes) {
			Destroy (existingCube);
		}

		//Next create all new cubes
		foreach (Vector3 postion in level.objects) {
			PlaceCube (postion);
		}

		//Next place the sun
		goal.transform.position = level.sunPosition;

		//Next initialize the user
		player.transform.position = level.playerPosition;
		player.GetComponent<Rigidbody> ().velocity = new Vector3(0, 0, 0);
		player.GetComponent<PlayerController> ().Clear ();
	}

	private void PlaceCube(Vector3 position) {
		GameObject newTarget = Instantiate (baseCube);
		newTarget.tag = "Well";
		newTarget.transform.position = position;
		newTarget.SetActive (true);
	}
}
