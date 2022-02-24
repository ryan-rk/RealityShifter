using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader Instance;

	[SerializeField] float loadSceneDelay = 1f;

	[SerializeField] GameObject playerPrefab;

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
		Instantiate(playerPrefab);
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

	IEnumerator DelayLoadingScene(int sceneIndex)
	{
		yield return new WaitForSeconds(loadSceneDelay);
		SceneManager.LoadScene(sceneIndex);
	}
}
