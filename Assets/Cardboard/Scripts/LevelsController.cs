using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelsController : MonoBehaviour {

	public GameObject baseSelector;
	void Start () {
		GenerateSelectors ();
	}

	public void HandleLevelSelection(int level) {
		//TODO
		PlayerPrefs.SetInt ("CurrentLevel", level);
		PlayerPrefs.Save ();
		Application.LoadLevel("DemoScene");
	}

	void GenerateSelectors() {
		foreach (Level level in LevelProvider.GetLevels()) {
			GenerateSelector (level);
		}
	}

	void GenerateSelector(Level level) {
		GameObject newSelector = Instantiate (baseSelector);
		newSelector.transform.position = GetPosition (level.level);
		Text text = newSelector.GetComponentInChildren<Text> ();
		text.text = level.name;
		Button button = newSelector.GetComponentInChildren<Button> ();
		button.onClick.AddListener(() => { HandleLevelSelection(level.level); });
	}

	Vector3 GetPosition(int level) {
		return new Vector3 (2 * level, 2, 5);
	}
}
