using UnityEngine;
using System.Collections;

public class StartMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGame() {
		Application.LoadLevel("DemoScene");
	}

	public void LoadSettings() {
		Application.LoadLevel ("SettingsScene");
	}
}
