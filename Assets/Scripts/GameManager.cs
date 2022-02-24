using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] GameObject playerPrefab;
	[SerializeField] float levelReloadDelay = 0.4f;

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

	public void SpawnPlayer()
	{
		Instantiate(playerPrefab);
	}

	public void WinLevel()
	{
		Debug.Log("Win level");
		LevelLoader.Instance.NextLevel();
	}

	public void ManagePlayerDeath()
	{
		StartCoroutine(DelayLevelReload());
	}

	IEnumerator DelayLevelReload()
	{
		yield return new WaitForSeconds(levelReloadDelay);
		LevelLoader.Instance.RestartLevel();
	}

}
