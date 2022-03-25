using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStage : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("LevelBGM", 0);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
