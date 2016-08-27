using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Place ();
	}

	void Place() {
		gameObject.transform.position = GenerateNewPosition ();
	}

	Vector3[] GetWellPositions() {
		GameObject[] wells = GameObject.FindGameObjectsWithTag ("Well");
		Vector3[] positions = new Vector3[wells.Length];
		for (int i = 0; i < wells.Length; i++) {
			positions [i] = wells [i].transform.position;
		}
		return positions;
	}

	float GetNearestWellDistance(Vector3 newPosition) {
		Vector3[] wells = GetWellPositions ();
		float min = float.MaxValue;
		foreach(Vector3 well in wells) {
			if (Vector3.Distance (newPosition, well) < min) {
				min = Vector3.Distance (newPosition, well);
			}
		}
		return min;
	}

	private Vector3 GenerateNewPosition() {
		Vector3 pos = Random.insideUnitSphere * 20.0f;
		pos.y = Mathf.Abs(pos.y);
		if (GetNearestWellDistance (pos) < 10) {
			return GenerateNewPosition ();
		}
		return pos;
	}


	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			GenerateNewPosition ();
			Handheld.Vibrate ();
			//TODO increment score
		}
	}
}
