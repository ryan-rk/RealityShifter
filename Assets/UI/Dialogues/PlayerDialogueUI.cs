using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueUI : DialogueUIController
{
	[SerializeField] Animator animator;
	[SerializeField] DialogueTrigger playerDialogueTrigger;
	[SerializeField] GameObject proceedIcon;
	public event Action OnDialogueUIEnd;

	// Start is called before the first frame update
	void Start()
	{
		DialogueManager.Instance.OnSentenceStartTyping += HideProceedIcon;
		DialogueManager.Instance.OnSentenceTyped += ShowProceedIcon;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void StartDialogueUI()
	{
		if (playerDialogueTrigger.dialogueBlocksQueue.Count == 0)
		{
			Debug.Log("No more conversation from player");
			return;
		}
		animator.Play("Open");
		DialogueManager.Instance.OnDialogueEnded += EndDialogueUI;
		DialogueManager.Instance.OnNextSentenceReady += UpdateDialogueText;
		playerDialogueTrigger.InitiateConversation();
	}

	public override void EndDialogueUI()
	{
		animator.Play("Close");
		DialogueManager.Instance.OnDialogueEnded -= EndDialogueUI;
		DialogueManager.Instance.OnNextSentenceReady -= UpdateDialogueText;
		OnDialogueUIEnd?.Invoke();
	}

	void ShowProceedIcon()
	{
		proceedIcon.SetActive(true);
	}

	void HideProceedIcon()
	{
		proceedIcon.SetActive(false);
	}

	private void OnDisable()
	{
		DialogueManager.Instance.OnSentenceStartTyping -= HideProceedIcon;
		DialogueManager.Instance.OnSentenceTyped -= ShowProceedIcon;
	}

}
