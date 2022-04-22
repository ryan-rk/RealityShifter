using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterScene : MonoBehaviour
{
	Player player;
	[SerializeField] float playerStopPoint = 0f;
	bool hasPlayerReachedStopPoint = false;

	[SerializeField] float dialogueEndDelay = 1f;

	[SerializeField] PlayerDialogueUI playerDialogueUI;
	[SerializeField] NarratorDialogueUI narratorDialogueUI;

	float bgmStopDelay = 0.1f;

	bool canDialogueProceed = false;
	int currentDialogueSequence = 0;

	// last inter scene settings
	[SerializeField] bool isLastInterScene = false;
	[SerializeField] GameObject secondGoal;
	[SerializeField] Vector2 secondGoalPosition;
	[SerializeField] float screenShakeIntensity;
	[SerializeField] float screenShakeDuration;

	enum DialogueOwner
	{
		Player, Narrator
	}

	private void OnEnable()
	{
		LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
		levelLoader.OnPlayerSpawned += PlayerEnteringScene;
	}

	void PlayerEnteringScene(Player player)
	{
		Debug.Log("player entering scene");
		this.player = player;
		player.horizontalMovement.normalizedMovement = 1f;
	}

	// Start is called before the first frame update
	void Start()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Intro", 0);
		}
		playerDialogueUI.OnDialogueUIEnd += SwitchDialogueToNarrator;
		narratorDialogueUI.OnDialogueUIEnd += SwitchDialogueToPlayer;
		narratorDialogueUI.OnNoMoreDialogueBlock += MovePlayerToGoal;
	}

	void MovePlayerToGoal()
	{
		Player player = GameObject.FindObjectOfType<Player>();
		player.horizontalMovement.normalizedMovement = isLastInterScene ? -1 : 1;
		narratorDialogueUI.OnNoMoreDialogueBlock -= MovePlayerToGoal;
		StartCoroutine(StopBGMAfterDelay());
	}

	IEnumerator StopBGMAfterDelay()
	{
		yield return new WaitForSeconds(bgmStopDelay);
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.StopSound("Intro", true);
		}
	}

	public void SkipInterScene()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Click");
		}
		StartCoroutine(StopBGMAfterDelay());
		LevelLoader.Instance.NextLevel();
	}

	void AwakePlayer(Player player)
	{
		Animator animator = player.GetComponentInChildren<Animator>();
		animator.Play("Awake");
		LevelLoader.Instance.OnPlayerSpawned -= AwakePlayer;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Select") && canDialogueProceed)
		{
			currentDialogueSequence += 1;
			DialogueManager.Instance.PrepareNextSentence();
			if (isLastInterScene)
			{
				if (currentDialogueSequence == 5)
				{
					if (secondGoal != null)
					{
						secondGoal.transform.position = secondGoalPosition;
					}
				}
				else if (currentDialogueSequence == 14)
				{
					if (CameraManager.Instance != null)
					{
						CameraManager.Instance.mainVcamScreenShaker.ScreenShake(screenShakeIntensity, screenShakeDuration);
					}
				}
			}
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

	void SwitchDialogueToPlayer()
	{
		canDialogueProceed = false;
		StartCoroutine(SwitchDialogueOwner(DialogueOwner.Player));
	}

	void SwitchDialogueToNarrator()
	{
		canDialogueProceed = false;
		StartCoroutine(SwitchDialogueOwner(DialogueOwner.Narrator));
	}

	IEnumerator SwitchDialogueOwner(DialogueOwner owner)
	{
		yield return new WaitForSeconds(dialogueEndDelay);
		switch (owner)
		{
			case DialogueOwner.Player:
				canDialogueProceed = true;
				playerDialogueUI.StartDialogueUI();
				break;

			case DialogueOwner.Narrator:
				canDialogueProceed = true;
				narratorDialogueUI.StartDialogueUI();
				break;
		}

	}

}
