using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
	public static SceneTransition Instance;

	[SerializeField] CircularTransition cTransition;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
	}

	public void InitializeTransition()
	{
		if (cTransition != null)
		{
			cTransition.gameObject.SetActive(true);
			cTransition.InstantShrink();
		}
	}

	public void TransitionIntoScene(float delay, Action transitionEndCallback)
	{
		if (cTransition != null && gameObject.activeInHierarchy)
		{
			cTransition.ExpandOut(delay, transitionEndCallback);
		}
		else
		{
			transitionEndCallback();
		}
		// StartCoroutine(DelayTransitionIn(delay, transitionEndCallback));
	}

	public void TransitionIntoSceneAtMouse(float delay, Action transitionEndCallback)
	{
		if (cTransition != null && gameObject.activeInHierarchy)
		{
			cTransition.isFollowMouse = true;
			cTransition.ExpandOut(delay, transitionEndCallback);
		}
		else
		{
			transitionEndCallback();
		}
		// StartCoroutine(DelayTransitionIn(delay, transitionEndCallback));
	}

	// IEnumerator DelayTransitionIn(float delay, Action transitionEndCallback)
	// {
	// 	yield return new WaitForSeconds(delay);
	// 	if (cTransition != null)
	// 	{
	// 		cTransition.ExpandOut();
	// 	}
	// 	transitionEndCallback();
	// }

	public void TransitionOutOfScene(float delay, Action transitionEndCallback)
	{
		if (cTransition != null && gameObject.activeInHierarchy)
		{
			cTransition.gameObject.SetActive(true);
			cTransition.ShrinkIn(delay, transitionEndCallback);
		}
		else
		{
			transitionEndCallback();
		}
		// StartCoroutine(DelayTransitionOut(delay, transitionEndCallback));
	}

	public void TransitionOutOfSceneAtMouse(float delay, Action transitionEndCallback)
	{
		if (cTransition != null && gameObject.activeInHierarchy)
		{
			cTransition.isFollowMouse = true;
			cTransition.gameObject.SetActive(true);
			cTransition.ShrinkIn(delay, transitionEndCallback);
		}
		else
		{
			transitionEndCallback();
		}
		// StartCoroutine(DelayTransitionOut(delay, transitionEndCallback));
	}

	// IEnumerator DelayTransitionOut(float delay, Action transitionEndCallback)
	// {
	// 	yield return new WaitForSeconds(delay);
	// 	if (cTransition != null)
	// 	{
	// 		cTransition.gameObject.SetActive(true);
	// 		cTransition.ShrinkIn();
	// 	}
	// 	transitionEndCallback();
	// }

}
