using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
	float transitionInDelay = 0.4f;
	float transitionOutDelay = 0.1f;

	private void Start()
	{
		SceneTransition.Instance.InitializeTransition();
		SceneTransition.Instance.TransitionIntoSceneAtMouse(transitionInDelay, () => { });
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("StartMenu", 0);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void BackToMainMenu()
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => SceneManager.LoadScene(0));
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Click");
		}
	}

	public void LoadStage(int stageNum)
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => SceneManager.LoadScene(stageNum + 2));
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Start");
			AudioManager.Instance.StopSound("StartMenu", true);
			if (stageNum >= 1 && stageNum <= 12)
			{
				AudioManager.Instance.PlaySound("LevelBGM", 0);
			}
		}
	}

}
