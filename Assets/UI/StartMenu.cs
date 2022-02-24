using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	float transitionInDelay = 0.4f;
	float transitionOutDelay = 0.1f;

	private void Start()
	{
		SceneTransition.Instance.TransitionIntoScene(transitionInDelay, () => { });
	}

	public void QuitGame()
	{
		SceneTransition.Instance.TransitionOutOfScene(transitionOutDelay, () => Application.Quit());
	}

	public void StartGame()
	{
		SceneTransition.Instance.TransitionOutOfScene(transitionOutDelay, StartFirstLevel);
	}

	void StartFirstLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
