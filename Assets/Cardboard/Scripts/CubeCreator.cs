using UnityEngine;
using System.Collections;

public class CubeCreator : MonoBehaviour {

	public GameObject baseTarget;
	// Use this for initialization
	void Start () {
		PlaceCubes ();
	}

	private void PlaceCubes() {
		GameObject newTarget;
		for (int i = 0; i < 3; i++) {
			//Vector3 pos = new Vector3 ((float) Random.Range (0, 10), 0f, (float) Random.Range (0, mHeight));
			Vector3 pos = Random.insideUnitSphere * 10.0f;
			pos.y = Mathf.Abs(pos.y);
			newTarget = Instantiate (baseTarget);
			newTarget.transform.position = pos;
			CubeController controller = newTarget.GetComponent<CubeController> ();
			//controller.Initialize ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
