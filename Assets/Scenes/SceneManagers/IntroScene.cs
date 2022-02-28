using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroScene : MonoBehaviour
{
	[SerializeField] float dialogueEndDelay = 1f;
	[SerializeField] DialogueTrigger playerDialogueTrigger;
	[SerializeField] Animator dialogueBoxAnimator;
	[SerializeField] TMP_Text playerText;

	[SerializeField] DialogueTrigger narratorDialogueTrigger;
	[SerializeField] NarratorDialogue narratorDialogue;

	bool canDialogueProceed = false;

	// Start is called before the first frame update
	void Start()
	{
		dialogueBoxAnimator.gameObject.SetActive(false);
		StartPlayerDialogue();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Jump") && canDialogueProceed)
		{
			DialogueManager.Instance.PrepareNextSentence();
		}
	}

	void StartPlayerDialogue()
	{
		if (playerDialogueTrigger.dialogueBlocksQueue.Count == 0)
		{
			Debug.Log("No more conversation from player");
			return;
		}
		dialogueBoxAnimator.gameObject.SetActive(true);
		canDialogueProceed = true;
		DialogueManager.Instance.OnDialogueEnded += EndPlayerDialogue;
		DialogueManager.Instance.OnNextSentenceReady += UpdateDialogueBoxText;
		playerDialogueTrigger.InitiateConversation();
	}

	void EndPlayerDialogue()
	{
		canDialogueProceed = false;
		dialogueBoxAnimator.Play("Close");
		DialogueManager.Instance.OnDialogueEnded -= EndPlayerDialogue;
		DialogueManager.Instance.OnNextSentenceReady -= UpdateDialogueBoxText;
		StartCoroutine(SwitchDialogueOwner(false));
	}

	void UpdateDialogueBoxText(string newSentence)
	{
		playerText.text = newSentence;
	}


	IEnumerator SwitchDialogueOwner(bool isPlayer)
	{
		yield return new WaitForSeconds(dialogueEndDelay);
		if (isPlayer)
		{
			StartPlayerDialogue();
		}
		else
		{
			StartNarratorDialogue();
		}

	}

	void StartNarratorDialogue()
	{
		if (narratorDialogueTrigger.dialogueBlocksQueue.Count == 0)
		{
			Debug.Log("No more conversation from narrator");
			Player player = GameObject.FindObjectOfType<Player>();
			player.horizontalMovement = 1;
			return;
		}
		narratorDialogue.StartDialogueUI();
		canDialogueProceed = true;
		DialogueManager.Instance.OnDialogueEnded += EndNarratorDialogue;
		DialogueManager.Instance.OnNextSentenceReady += narratorDialogue.UpdateDialogueText;
		narratorDialogueTrigger.InitiateConversation();
	}

	void EndNarratorDialogue()
	{
		narratorDialogue.EndDialogueUI();
		DialogueManager.Instance.OnDialogueEnded -= EndNarratorDialogue;
		DialogueManager.Instance.OnNextSentenceReady -= narratorDialogue.UpdateDialogueText;
		StartCoroutine(SwitchDialogueOwner(true));
	}

}
