using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}

	public void WinLevel()
	{
		Debug.Log("Win level");
	}

	public void ManagePlayerDeath()
	{
		LevelLoader.Instance.RestartLevel();
	}
}
