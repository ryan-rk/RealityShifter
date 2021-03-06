using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorDialogueUI : DialogueUIController
{
	[SerializeField] ParticleSystem textParticle;
	[SerializeField] Animator animator;
	[SerializeField] DialogueTrigger narratorDialogueTrigger;
	public event Action OnDialogueUIEnd;
	public event Action OnNoMoreDialogueBlock;

	public override void StartDialogueUI()
	{
		if (narratorDialogueTrigger.dialogueBlocksQueue.Count == 0)
		{
			Debug.Log("No more conversation from narrator");
			OnNoMoreDialogueBlock?.Invoke();
			return;
		}
		animator.Play("FadeIn");
		textParticle.Play();
		DialogueManager.Instance.OnDialogueEnded += EndDialogueUI;
		DialogueManager.Instance.OnNextSentenceReady += UpdateDialogueText;
		narratorDialogueTrigger.InitiateConversation();
	}

	public override void EndDialogueUI()
	{
		animator.Play("FadeOut");
		textParticle.Stop();
		DialogueManager.Instance.OnDialogueEnded -= EndDialogueUI;
		DialogueManager.Instance.OnNextSentenceReady -= UpdateDialogueText;
		OnDialogueUIEnd?.Invoke();
	}

}
