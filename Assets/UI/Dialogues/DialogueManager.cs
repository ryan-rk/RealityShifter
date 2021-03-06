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
	public event Action OnSentenceStartTyping;
	public event Action OnSentenceTyped;

	WaitForSeconds typingIntervalWait = new WaitForSeconds(0.02f);

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
		// OnNextSentenceReady?.Invoke(nextSentence);
		StopAllCoroutines();
		StartCoroutine(TypingSentence(nextSentence));
	}

	IEnumerator TypingSentence(string sentence)
	{
		OnSentenceStartTyping?.Invoke();
		string currentSentence = "";
		OnNextSentenceReady?.Invoke(currentSentence);
		foreach (char letter in sentence.ToCharArray())
		{
			yield return typingIntervalWait;
			currentSentence += letter;
			OnNextSentenceReady?.Invoke(currentSentence);
		}
		OnSentenceTyped?.Invoke();
	}

	void EndDialogue()
	{
		OnDialogueEnded?.Invoke();
	}
}
