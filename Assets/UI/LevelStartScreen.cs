using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScreen : MonoBehaviour
{
	public void StartPlaying()
	{
		LevelLoader.Instance.InitializeLevel();
		gameObject.SetActive(false);
	}
}
