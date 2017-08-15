using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public GameObject baseCube;
	public GameObject baseDrift;
	public GameObject player;
	public GameObject goal;
	public GameObject backMenu;

	public void LoadLevel(Level level) {
		foreach (Vector3 postion in level.objects) {
			PlaceCube (postion);
		}

		PlaceBackMenu (level.playerPosition);

		if (ShouldDoDrift ()) {
			foreach (Vector3 position in level.drifts) {
				PlaceDrift (position);
			}
		}

		goal.transform.position = level.sunPosition;

		player.transform.position = level.playerPosition;
	}

	private void PlaceCube(Vector3 position) {
		GameObject newTarget = Instantiate (baseCube);
		newTarget.tag = "Well";
		newTarget.transform.position = position;
		newTarget.SetActive (true);
	}

	private void PlaceDrift(Vector3 postion) {
		GameObject newDrift = Instantiate (baseDrift);
		newDrift.tag = "Drift";
		newDrift.transform.position = postion;
		newDrift.SetActive (false);
	}

	private bool ShouldDoDrift() {
		return PlayerPrefs.GetInt ("ParticlesOff") == 0;
	}

	private void PlaceBackMenu(Vector3 playerPosition) {
		Vector3 menuPosition = new Vector3 (playerPosition.x, playerPosition.y - 3, playerPosition.z + .5f);
		backMenu.transform.position = menuPosition;
	}
}
