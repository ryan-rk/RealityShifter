using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleStage : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		string[] sceneName = SceneManager.GetActiveScene().name.Split('_');
		string worldNum = sceneName[1];
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("World" + worldNum, 0);
			// AudioManager.Instance.PlaySound("LevelBGM", 0);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
