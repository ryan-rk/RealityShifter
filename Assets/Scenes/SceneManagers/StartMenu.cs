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
		SceneTransition.Instance.InitializeTransition();
		SceneTransition.Instance.TransitionIntoScene(transitionInDelay, () => { });
		AudioManager.Instance.PlaySound("StartMenu", 0);
	}

	public void QuitGame()
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => Application.Quit());
		AudioManager.Instance.PlaySound("Click");
	}

	public void StartGame()
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, StartFirstLevel);
		AudioManager.Instance.PlaySound("Start");
	}

	void StartFirstLevel()
	{
		// AudioManager.Instance.StopSound("StartMenu", true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
