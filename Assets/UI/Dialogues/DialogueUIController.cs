using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUIController : MonoBehaviour
{
	[SerializeField] protected TMP_Text dialogueText;

	public virtual void StartDialogueUI()
	{
	}

	public virtual void EndDialogueUI()
	{
	}

	public void UpdateDialogueText(string newSentence)
	{
		dialogueText.text = newSentence;
	}
}
