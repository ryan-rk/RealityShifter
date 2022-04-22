using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
	[SerializeField] float sceneLoadDelay = 1f;
	[SerializeField] float playerFirstDialogueDelay = 2f;
	[SerializeField] float dialogueEndDelay = 1f;

	[SerializeField] float screenShakeIntensity = 2f;
	[SerializeField] float screenShakeDuration = 0.2f;
	[SerializeField] float screenShakeFrequency = 10f;
	// [SerializeField] DialogueTrigger playerDialogueTrigger;
	// [SerializeField] Animator dialogueBoxAnimator;
	// [SerializeField] TMP_Text playerText;

	// [SerializeField] DialogueTrigger narratorDialogueTrigger;
	[SerializeField] PlayerDialogueUI playerDialogueUI;
	[SerializeField] NarratorDialogueUI narratorDialogueUI;
	[SerializeField] UIShaker playerDialogueShaker;

	float bgmStopDelay = 0.1f;

	bool canDialogueProceed = false;
	int currentDialogueSequence = 0;

	enum DialogueOwner
	{
		Player, Narrator
	}

	private void OnEnable()
	{
		LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
		levelLoader.OnPlayerSpawned += AwakePlayer;
	}
	// Start is called before the first frame update
	void Start()
	{
		// dialogueBoxAnimator.gameObject.SetActive(false);
		// StartPlayerDialogue();
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Intro", 0);
		}
		StartCoroutine(DelayIntroCutscene());
		playerDialogueUI.OnDialogueUIEnd += SwitchDialogueToNarrator;
		narratorDialogueUI.OnDialogueUIEnd += SwitchDialogueToPlayer;
		narratorDialogueUI.OnNoMoreDialogueBlock += MovePlayerToGoal;
	}

	void MovePlayerToGoal()
	{
		Player player = GameObject.FindObjectOfType<Player>();
		player.horizontalMovement.normalizedMovement = 1;
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

	public void SkipIntroScene()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Click");
			// AudioManager.Instance.PlaySound("LevelBGM", 0);
		}
		StartCoroutine(StopBGMAfterDelay());
		LevelLoader.Instance.NextLevel();
	}

	IEnumerator DelayIntroCutscene()
	{
		yield return new WaitForSeconds(sceneLoadDelay);
		StartCoroutine(DelayStartingDialogue());
	}

	void AwakePlayer(Player player)
	{
		Animator animator = player.GetComponentInChildren<Animator>();
		animator.Play("Awake");
		LevelLoader.Instance.OnPlayerSpawned -= AwakePlayer;
	}

	IEnumerator DelayStartingDialogue()
	{
		yield return new WaitForSeconds(playerFirstDialogueDelay);
		canDialogueProceed = true;
		playerDialogueUI.StartDialogueUI();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Select") && canDialogueProceed)
		{
			currentDialogueSequence += 1;
			DialogueManager.Instance.PrepareNextSentence();
			if (currentDialogueSequence == 4)
			{
				playerDialogueShaker.UIShake(screenShakeIntensity, screenShakeDuration, screenShakeFrequency);
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


	// void StartPlayerDialogue()
	// {
	// 	if (playerDialogueTrigger.dialogueBlocksQueue.Count == 0)
	// 	{
	// 		Debug.Log("No more conversation from player");
	// 		return;
	// 	}
	// 	// dialogueBoxAnimator.gameObject.SetActive(true);
	// 	canDialogueProceed = true;
	// 	DialogueManager.Instance.OnDialogueEnded += EndPlayerDialogue;
	// 	DialogueManager.Instance.OnNextSentenceReady += UpdateDialogueBoxText;
	// 	playerDialogueTrigger.InitiateConversation();
	// }

	// void EndPlayerDialogue()
	// {
	// 	canDialogueProceed = false;
	// 	// dialogueBoxAnimator.Play("Close");
	// 	DialogueManager.Instance.OnDialogueEnded -= EndPlayerDialogue;
	// 	DialogueManager.Instance.OnNextSentenceReady -= UpdateDialogueBoxText;
	// 	StartCoroutine(SwitchDialogueOwner(false));
	// }

	// void UpdateDialogueBoxText(string newSentence)
	// {
	// 	// playerText.text = newSentence;
	// }


	// void StartNarratorDialogue()
	// {
	// 	if (narratorDialogueTrigger.dialogueBlocksQueue.Count == 0)
	// 	{
	// 		Debug.Log("No more conversation from narrator");
	// 		Player player = GameObject.FindObjectOfType<Player>();
	// 		player.horizontalMovement = 1;
	// 		return;
	// 	}
	// 	narratorDialogueUI.StartDialogueUI();
	// 	canDialogueProceed = true;
	// 	DialogueManager.Instance.OnDialogueEnded += EndNarratorDialogue;
	// 	DialogueManager.Instance.OnNextSentenceReady += narratorDialogueUI.UpdateDialogueText;
	// 	narratorDialogueTrigger.InitiateConversation();
	// }

	// void EndNarratorDialogue()
	// {
	// 	narratorDialogueUI.EndDialogueUI();
	// 	DialogueManager.Instance.OnDialogueEnded -= EndNarratorDialogue;
	// 	DialogueManager.Instance.OnNextSentenceReady -= narratorDialogueUI.UpdateDialogueText;
	// 	StartCoroutine(SwitchDialogueOwner(true));
	// }

}
