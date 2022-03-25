using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	[SerializeField] Dialogue[] dialogueBlocks;
	public Queue<Dialogue> dialogueBlocksQueue { get; private set; } = new Queue<Dialogue>();

	private void Awake()
	{
		foreach (Dialogue dialogueBlock in dialogueBlocks)
		{
			dialogueBlocksQueue.Enqueue(dialogueBlock);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void InitiateConversation()
	{
		Debug.Log("Conversation initiated");
		DialogueManager.Instance.StartDialogue(dialogueBlocksQueue.Dequeue());
	}
}
