using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader Instance;

	[SerializeField] float transitionInDelay = 0.4f;

	[SerializeField] float nextLevelDelay = 1f;
	[SerializeField] float restartLevelDelay = 0f;

	[SerializeField] Player playerPrefab;
	public Vector2 playerSpawnPoint;
	[SerializeField] bool isDrawSpawnPoint;

	private void Awake()
	{
		Instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		LevelStartScreen tutorialScreen = FindObjectOfType<LevelStartScreen>();
		if (!GameManager.isLevelFirstEntered)
		{
			tutorialScreen?.StartPlaying();
		}
		if (tutorialScreen == null)
		{
			InitializeLevel();
		}
	}

	public void InitializeLevel()
	{
		Player playerInstance = Instantiate(playerPrefab);
		playerInstance.Spawn(LevelLoader.Instance.playerSpawnPoint);
		SceneTransition.Instance.InitializeTransition();
		SceneTransition.Instance.TransitionIntoScene(transitionInDelay, () => { });
		GameManager.isLevelFirstEntered = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void RestartLevel()
	{
		Action reloadSceneAction = () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		SceneTransition.Instance.TransitionOutOfScene(restartLevelDelay, reloadSceneAction);
	}

	// void ReloadScene()
	// {
	// 	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	// }

	public void NextLevel()
	{
		SceneTransition.Instance.TransitionOutOfScene(nextLevelDelay, NextLevelChecker);
	}

	void NextLevelChecker()
	{
		if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1))
		{
			Debug.Log("Game Completed");
		}
		else
		{
			GameManager.isLevelFirstEntered = true;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	private void OnDrawGizmos()
	{
		if (isDrawSpawnPoint)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawIcon(playerSpawnPoint, "Record Off", true);
			UnityEditor.EditorGUIUtility.IconContent("Record Off");
		}
	}
}
