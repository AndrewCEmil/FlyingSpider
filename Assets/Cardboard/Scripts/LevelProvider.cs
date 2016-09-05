using UnityEngine;
using System.Collections;

public class LevelProvider : MonoBehaviour {

	public static Level GetLevel(int level) {
		if (level == 0) {
			return LevelZero ();
		} if (level == 1) {
			return LevelOne ();
		} else if (level == 2) {
			return LevelTwo ();
		}
		return null;
	}

	public static Level LevelOne() {
		Vector3[] positions = new Vector3[5];
		positions [0] = new Vector3 (10, 10, 10);
		positions [1] = new Vector3 (-10, 10, 10);
		positions [2] = new Vector3 (-10, 10, -10);
		positions [3] = new Vector3 (10, 10, -10);
		positions [4] = new Vector3 (2, 2, 2);
		Level levelZero = new Level ();
		levelZero.objects = positions;
		levelZero.playerPosition = new Vector3 (0, 0, 0);
		levelZero.sunPosition = new Vector3 (2, 0, 2);
		return levelZero;
	}

	public static Level LevelTwo() {
		Vector3[] positions = new Vector3[4];
		positions [0] = new Vector3 (10, 10, 10);
		positions [1] = new Vector3 (-10, 10, 10);
		positions [2] = new Vector3 (-10, 10, -10);
		positions [3] = new Vector3 (10, 10, -10);
		Level levelOne = new Level ();
		levelOne.objects = positions;
		levelOne.playerPosition = new Vector3 (0, 0, 0);
		levelOne.sunPosition = new Vector3 (0, 5, 0);
		return levelOne;
	}

	public static Level LevelZero() {
		return null;
	}
}
