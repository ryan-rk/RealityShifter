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
			AudioManager.Instance.StopSound("LevelBGM", true);
		}
	}

	public void LoadStage(int stageNum)
	{
		SceneTransition.Instance.TransitionOutOfSceneAtMouse(transitionOutDelay, () => SceneManager.LoadScene(stageNum + 1));
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Start");
			AudioManager.Instance.StopSound("StartMenu", true);
			AudioManager.Instance.PlaySound("LevelBGM", 0);
		}
	}

}
