using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroScene : MonoBehaviour
{
	[SerializeField] float playerStopPoint = 0f;
	Player player;
	bool hasPlayerReachedStopPoint = false;
	[SerializeField] float dialogueEndDelay = 0f;

	[SerializeField] PlayerDialogueUI playerDialogueUI;
	[SerializeField] NarratorDialogueUI narratorDialogueUI;
	[SerializeField] Exit exit;

	[SerializeField] float narratorEndToGlithDelay = 1f;
	[SerializeField] float glitchDuration = 0.1f;
	[SerializeField] float glitchInterval = 0.5f;
	[SerializeField] float glitchScreenShakeIntensity = 5f;
	[SerializeField] float glitchScreenShakeDuration = 0.2f;
	[SerializeField] float sceneTransitionOutDelay = 2f;

	[SerializeField] GameObject backgroundFG;
	[SerializeField] GameObject backgroundMasks;
	[SerializeField] GameObject realSign;
	[SerializeField] GameObject fakeSign;
	[SerializeField] ParticleSystem exitParticle;
	[SerializeField] Color fakeExitParticleColor;

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
		// dialogueBoxAnimator.gameObject.SetActive(false);
		// StartPlayerDialogue();
		playerDialogueUI.OnDialogueUIEnd += PlayerExitingScene;
		narratorDialogueUI.OnDialogueUIEnd += StartOutroEndingScene;
		exit.OnPlayerReachedExit += SwitchDialogueToNarrator;
	}

	void PlayerEnteringScene(Player player)
	{
		Debug.Log("player entering scene");
		this.player = player;
		player.horizontalMovement = 1f;
	}

	void PlayerExitingScene()
	{
		player.horizontalMovement = 1f;
		playerDialogueUI.OnDialogueUIEnd -= PlayerExitingScene;
	}

	void StartOutroEndingScene()
	{
		Debug.Log("Starting outro ending scene");
		StartCoroutine(OutroEndingSequence());
	}

	IEnumerator OutroEndingSequence()
	{
		yield return new WaitForSeconds(narratorEndToGlithDelay);
		CameraManager.Instance.mainVcamScreenShaker.ScreenShake(glitchScreenShakeIntensity, glitchScreenShakeDuration);
		backgroundMasks.SetActive(true);
		yield return new WaitForSeconds(glitchDuration);
		backgroundMasks.SetActive(false);
		yield return new WaitForSeconds(glitchInterval);
		CameraManager.Instance.mainVcamScreenShaker.ScreenShake(glitchScreenShakeIntensity, glitchScreenShakeDuration);
		backgroundMasks.SetActive(true);
		yield return new WaitForSeconds(glitchDuration);
		backgroundMasks.SetActive(false);
		yield return new WaitForSeconds(glitchInterval);
		CameraManager.Instance.mainVcamScreenShaker.ScreenShake(glitchScreenShakeIntensity, glitchScreenShakeDuration);
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("ShiftDown");
		}
		backgroundFG.SetActive(false);
		realSign.SetActive(false);
		fakeSign.SetActive(true);
		var mainModule = exitParticle.main;
		mainModule.startColor = fakeExitParticleColor;
		yield return new WaitForSeconds(sceneTransitionOutDelay);
		LevelLoader.Instance.NextLevel();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Jump") && canDialogueProceed)
		{
			currentDialogueSequence += 1;
			DialogueManager.Instance.PrepareNextSentence();
		}

		if (player != null)
		{
			if (player.transform.position.x >= playerStopPoint && !hasPlayerReachedStopPoint)
			{
				hasPlayerReachedStopPoint = true;
				player.horizontalMovement = 0f;
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
