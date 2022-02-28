using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarratorDialogue : MonoBehaviour
{
	[SerializeField] ParticleSystem textParticle;
	[SerializeField] Animator animator;
	[SerializeField] TMP_Text dialogueText;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartDialogueUI()
	{
		animator.Play("FadeIn");
		textParticle.Play();
	}

	public void EndDialogueUI()
	{
		animator.Play("FadeOut");
		textParticle.Stop();
	}

	public void UpdateDialogueText(string newSentence)
	{
		dialogueText.text = newSentence;
	}

}
