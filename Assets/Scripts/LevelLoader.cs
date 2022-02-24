using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader Instance;

	[SerializeField] float loadSceneDelay = 1f;

	private void Awake()
	{
		Instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		InitializeLevel();
	}

	void InitializeLevel()
	{
		GameManager.Instance.SpawnPlayer();
		SceneTransition.Instance.TransitionIntoScene();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void RestartLevel()
	{
		SceneTransition.Instance.TransitionOutOfScene();
		StartCoroutine(DelayLoadingScene(SceneManager.GetActiveScene().buildIndex));
	}

	public void NextLevel()
	{
		SceneTransition.Instance.TransitionOutOfScene();
		if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1))
		{
			Debug.Log("Game Completed");
		}
		else
		{
			StartCoroutine(DelayLoadingScene(SceneManager.GetActiveScene().buildIndex + 1));
		}
	}

	IEnumerator DelayLoadingScene(int sceneIndex)
	{
		yield return new WaitForSeconds(loadSceneDelay);
		SceneManager.LoadScene(sceneIndex);
	}
}
