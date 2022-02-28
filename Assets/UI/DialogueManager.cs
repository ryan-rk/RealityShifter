using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance;
	Queue<string> sentences = new Queue<string>();
	public event Action<string> OnNextSentenceReady;
	public event Action OnDialogueEnded;

	private void Awake()
	{
		Instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
	}

	public void StartDialogue(Dialogue dialogue)
	{
		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		PrepareNextSentence();
	}

	public void PrepareNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string nextSentence = sentences.Dequeue();
		Debug.Log("show next sentence");
		OnNextSentenceReady?.Invoke(nextSentence);
	}

	void EndDialogue()
	{
		Debug.Log("Dialogue ended");
		OnDialogueEnded?.Invoke();
	}
}
