using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularTransition : MonoBehaviour
{
	public bool isFollowMouse = false;
	[SerializeField] RectTransform canvasRectTransform;
	[SerializeField] Image transitionOverlay;
	[SerializeField] Image mask;
	[SerializeField] float shrinkSpeedRelativeToWidth = 0.01f;
	[SerializeField] float expandSpeedRelativeToWidth = 0.01f;
	float expandedRadius = 2.5f;
	bool isChangingSize = false;

	private void OnEnable()
	{
		// Make the transition overlay to be twice as large as the width and height to account for every extreme position of mask on screen
		transitionOverlay.rectTransform.sizeDelta = new Vector2(2.1f * Screen.width, 2.1f * Screen.height) / canvasRectTransform.localScale.x;
		InstantExpand();
	}

	public void ShrinkIn(float delay, Action transitionEndCallback)
	{
		Player player = FindObjectOfType<Player>();
		if (player != null && !isFollowMouse)
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(player.transform.position) / canvasRectTransform.localScale.x;
		}
		else if (isFollowMouse)
		{
			mask.rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
		}
		else
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(Vector3.zero);
		}
		StartCoroutine(SizeChangingProcess(delay, 0, shrinkSpeedRelativeToWidth * Screen.width, false, transitionEndCallback));
	}

	public void InstantShrink()
	{
		mask.rectTransform.sizeDelta = Vector2.zero;
	}

	public void ExpandOut(float delay, Action transitionEndCallback)
	{
		Player player = FindObjectOfType<Player>();
		if (player != null && !isFollowMouse)
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(player.transform.position) / canvasRectTransform.localScale.x;
		}
		else if (isFollowMouse)
		{
			mask.rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
		}
		else
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(Vector3.zero);
		}
		StartCoroutine(SizeChangingProcess(delay, expandedRadius * Screen.width, expandSpeedRelativeToWidth * Screen.width, true, transitionEndCallback));
	}

	void InstantExpand()
	{
		mask.rectTransform.sizeDelta = new Vector2(expandedRadius * Screen.width, expandedRadius * Screen.width);
	}

	IEnumerator SizeChangingProcess(float delay, float targetRadius, float changeSpeed, bool disableOnFinish, Action transitionEndCallback)
	{
		yield return new WaitForSeconds(delay);
		if (!isChangingSize)
		{
			isChangingSize = true;
			float radius = mask.rectTransform.sizeDelta.x;
			while (radius != targetRadius)
			{
				radius = Mathf.MoveTowards(radius, targetRadius, changeSpeed * Time.deltaTime);
				mask.rectTransform.sizeDelta = new Vector2(radius, radius);
				yield return null;
			}
			isChangingSize = false;
			if (disableOnFinish)
			{
				gameObject.SetActive(false);
			}
			transitionEndCallback();
		}
	}
}
