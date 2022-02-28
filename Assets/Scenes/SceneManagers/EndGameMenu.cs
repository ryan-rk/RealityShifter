using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
	float transitionInDelay = 0.4f;
	float transitionOutDelay = 0.1f;

	private void Start()
	{
		SceneTransition.Instance.InitializeTransition();
		SceneTransition.Instance.TransitionIntoScene(transitionInDelay, () => { });
	}

	public void QuitGame()
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => Application.Quit());
	}

	public void BackToMainMenu()
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => SceneManager.LoadScene(0));
	}

}
