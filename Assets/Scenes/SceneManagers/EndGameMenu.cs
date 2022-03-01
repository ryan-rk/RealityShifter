using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
	float transitionInDelay = 2f;
	float transitionOutDelay = 1f;
	[SerializeField] GameObject overlayUI;
	[SerializeField] float showTitleDelay = 3f;
	[SerializeField] float titleFadeInSpeed = 1f;
	[SerializeField] CanvasGroup titleCanvasGroup;

	private void Start()
	{
		StartCoroutine(StartEndingSequence());
	}

	public void QuitGame()
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => Application.Quit());
	}

	public void BackToMainMenu()
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => SceneManager.LoadScene(0));
	}

	IEnumerator StartEndingSequence()
	{
		yield return new WaitForSeconds(transitionInDelay);
		overlayUI.SetActive(true);
		StartCoroutine(ShowTitle());
	}

	IEnumerator ShowTitle()
	{
		yield return new WaitForSeconds(showTitleDelay);
		while (titleCanvasGroup.alpha < 1)
		{
			yield return null;
			titleCanvasGroup.alpha += titleFadeInSpeed * Time.deltaTime;
		}
		Debug.Log("Title fade in completed");
	}

}
