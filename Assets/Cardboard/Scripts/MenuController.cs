using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Slider musicSlider;
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

	public void LoadStart() {
		Application.LoadLevel ("StartScene");
	}

	public void LoadLevelSelection() {
		Application.LoadLevel ("LevelScene");
	}

	public void SetMusicVolume() {
		AudioSource musicPlayer = GameObject.Find ("MusicPlayer").GetComponent<AudioSource> ();
		musicPlayer.volume = musicSlider.value;
	}
}
