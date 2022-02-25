using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public static bool isPaused = false;

	[SerializeField] GameObject pauseMenuUI;
	Animator pauseMenuUIAnimator;
	[SerializeField] float gameResumeDelay = 0.5f;

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
		isPaused = true;
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{
		pauseMenuUIAnimator.Play("Close");
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
	}

	public void ReturnToStageSelect()
	{
		Debug.Log("Returning to stage select");
	}
}
