using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public static bool isPaused = false;

	[SerializeField] GameObject pauseMenuUI;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PauseGame()
	{
		isPaused = true;
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{
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
