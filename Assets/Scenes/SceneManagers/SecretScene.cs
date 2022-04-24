using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretScene : MonoBehaviour
{
	[SerializeField] float playerStopPoint = 0f;
	Player player;
	bool hasPlayerReachedStopPoint = false;

	[SerializeField] PlayerDialogueUI playerDialogueUI;
	[SerializeField] NarratorDialogueUI narratorDialogueUI;
	[SerializeField] Exit exit;
	[SerializeField] float sceneTransitionOutDelay = 3f;

	bool canDialogueProceed = false;
	int currentDialogueSequence = 0;

	enum DialogueOwner
	{
		Player, Narrator
	}

	private void OnEnable()
	{
		LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
		levelLoader.OnPlayerSpawned += PlayerEnteringScene;
	}

	// Start is called before the first frame update
	void Start()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Intro", 0);
		}
		playerDialogueUI.OnDialogueUIEnd += PlayerExitingScene;
		exit.OnPlayerReachedExit += StartOutroEndingScene;
	}

	void PlayerEnteringScene(Player player)
	{
		Debug.Log("player entering scene");
		this.player = player;
		player.horizontalMovement.normalizedMovement = 1f;
	}

	void PlayerExitingScene()
	{
		player.horizontalMovement.normalizedMovement = 1f;
		playerDialogueUI.OnDialogueUIEnd -= PlayerExitingScene;
	}

	void StartOutroEndingScene()
	{
		Debug.Log("Starting outro ending scene");
		StartCoroutine(OutroEndingSequence());
	}

	IEnumerator OutroEndingSequence()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.StopSound("Intro", true);
		}
		yield return new WaitForSeconds(sceneTransitionOutDelay);
		LevelLoader.Instance.NextLevel();
	}

	public void SkipOutroScene()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Click");
			AudioManager.Instance.StopSound("Intro", true);
		}
		LevelLoader.Instance.NextLevel();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Select") && canDialogueProceed)
		{
			currentDialogueSequence += 1;
			DialogueManager.Instance.PrepareNextSentence();
		}

		if (player != null)
		{
			if (player.transform.position.x >= playerStopPoint && !hasPlayerReachedStopPoint)
			{
				hasPlayerReachedStopPoint = true;
				player.horizontalMovement.normalizedMovement = 0f;
				canDialogueProceed = true;
				playerDialogueUI.StartDialogueUI();
			}
		}
	}
}
