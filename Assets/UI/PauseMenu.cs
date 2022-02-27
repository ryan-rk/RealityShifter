using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool isPaused = false;

	[SerializeField] GameObject pauseMenuUI;
	Animator pauseMenuUIAnimator;
	[SerializeField] float gameResumeDelay = 0.5f;
	[SerializeField] float returnToMainMenuDelay = 0f;

	// Start is called before the first frame update
	void Start()
	{
		pauseMenuUIAnimator = pauseMenuUI.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PauseGame()
	{
		StopAllCoroutines();
		AudioManager.Instance.PlaySound("Click");
		isPaused = true;
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{
		pauseMenuUIAnimator.Play("Close");
		AudioManager.Instance.PlaySound("Click");
		StartCoroutine(DelayResumingGame());
	}

	IEnumerator DelayResumingGame()
	{
		yield return new WaitForSecondsRealtime(gameResumeDelay);
		isPaused = false;
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}

	public void ReturnToMainMenu()
	{
		Debug.Log("Returning to main menu");
		Time.timeScale = 1f;
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(returnToMainMenuDelay, () => SceneManager.LoadScene(0));
		AudioManager.Instance.PlaySound("Click");
	}

	public void ReturnToStageSelect()
	{
		Debug.Log("Returning to stage select");
		Time.timeScale = 1f;
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(returnToMainMenuDelay, () => SceneManager.LoadScene(1));
		AudioManager.Instance.PlaySound("Click");
		AudioManager.Instance.StopSound("LevelBGM", true);
		AudioManager.Instance.PlaySound("StartMenu", 0);
	}
}
