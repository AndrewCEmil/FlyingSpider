using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelProvider : MonoBehaviour {

	public static Dictionary<int,bool> GetLevelLocks() {
		string levelLocksJson = PlayerPrefs.GetString ("levelLocks");
		if (levelLocksJson.Equals ("")) {
			InitLevelLocks ();
		}
		Dictionary<int,bool> levelLocks = JsonUtility.FromJson<Dictionary<int,bool>>(levelLocksJson);
		return levelLocks;
	}

	public static void SetLevelLocks(Dictionary<int, bool> levelLocks) {
		PlayerPrefs.SetString ("levelLocks", JsonUtility.ToJson (levelLocks));
	}

	public static void OpenLevelLock (int level) {
		Dictionary<int, bool> levelLocks = GetLevelLocks ();
		levelLocks [level] = true;
	}

	public static string InitLevelLocks() {
		Dictionary<int, bool> lockLevels = new Dictionary<int,bool> ();
		for (int i = 1; i <= NumLevels (); i++) {
			lockLevels [i] = false;
		}
		lockLevels [1] = true;
		SetLevelLocks (lockLevels);
		return JsonUtility.ToJson (lockLevels);
	}

	public static bool IsLevelLocked(int level) {
		return GetLevelLocks () [level];
	}

	public static Level[] GetLevels() {
		Level[] levels = new Level[NumLevels ()];
		for (int i = 1; i <= NumLevels (); i++) {
			levels [i - 1] = GetLevel (i);
		}
		return levels;
	}


	public static Level GetLevel(int level) {
		if (level == 0) {
			return LevelOne ();
		} if (level == 1) {
			return LevelOne ();
		} else if (level == 2) {
			return LevelTwo ();
		}
		return null;
	}

	static int NumLevels() {
		return 2;
	}

	public static Level LevelOne() {
		Vector3[] positions = new Vector3[5];
		positions [0] = new Vector3 (10, 10, 10);
		positions [1] = new Vector3 (-10, 10, 10);
		positions [2] = new Vector3 (-10, 10, -10);
		positions [3] = new Vector3 (10, 10, -10);
		positions [4] = new Vector3 (2, 2, 2);
		Level level = new Level ();
		level.objects = positions;
		level.playerPosition = new Vector3 (0, 0.5f, 0);
		level.sunPosition = new Vector3 (2, 0, 2);
		level.name = "ONE";
		level.level = 1;
		level.locked = IsLevelLocked (level.level);
		return level;
	}

	public static Level LevelTwo() {
		Vector3[] positions = new Vector3[4];
		positions [0] = new Vector3 (10, 10, 10);
		positions [1] = new Vector3 (-10, 10, 10);
		positions [2] = new Vector3 (-10, 10, -10);
		positions [3] = new Vector3 (10, 10, -10);
		Level level = new Level ();
		level.objects = positions;
		level.playerPosition = new Vector3 (0, 0.5f, 0);
		level.sunPosition = new Vector3 (0, 5, 0);
		level.name = "TWO";
		level.level = 2;
		level.locked = IsLevelLocked (level.level);
		return level;
	}

	public static Level LevelZero() {
		return null;
	}
}
