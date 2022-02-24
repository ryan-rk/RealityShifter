using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
	public static SceneTransition Instance;

	[SerializeField] CircularTransition cTransition;
	[SerializeField] float transitionInDelay = 0.5f;
	[SerializeField] float transitionOutDelay = 0.2f;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		if (cTransition != null)
		{
			cTransition.gameObject.SetActive(true);
			cTransition.InstantShrink();
		}
	}

	public void TransitionIntoScene()
	{
		StartCoroutine(DelayTransitionIn());
	}

	IEnumerator DelayTransitionIn()
	{
		yield return new WaitForSeconds(transitionInDelay);
		if (cTransition != null)
		{
			cTransition.ExpandOut();
		}
	}

	public void TransitionOutOfScene()
	{
		StartCoroutine(DelayTransitionOut());
	}

	IEnumerator DelayTransitionOut()
	{
		yield return new WaitForSeconds(transitionOutDelay);
		if (cTransition != null)
		{
			cTransition.gameObject.SetActive(true);
			cTransition.ShrinkIn();
		}
	}

}
