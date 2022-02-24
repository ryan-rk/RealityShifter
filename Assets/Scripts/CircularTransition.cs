using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularTransition : MonoBehaviour
{
	[SerializeField] Image transitionOverlay;
	[SerializeField] Image mask;
	[SerializeField] float shrinkSpeedRelativeToWidth = 0.01f;
	[SerializeField] float expandSpeedRelativeToWidth = 0.01f;
	float expandedRadius = 2.5f;
	bool isChangingSize = false;

	private void OnEnable()
	{
		// Make the transition overlay to be twice as large as the width and height to account for every extreme position of mask on screen
		transitionOverlay.rectTransform.sizeDelta = new Vector2(2.1f * Screen.width, 2.1f * Screen.height);
		InstantExpand();
	}

	public void ShrinkIn()
	{
		Player player = FindObjectOfType<Player>();
		if (player != null)
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(player.transform.position);
		}
		else
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(Vector3.zero);
		}
		StartCoroutine(SizeChangingProcess(0, shrinkSpeedRelativeToWidth * Screen.width, false));
	}

	public void InstantShrink()
	{
		mask.rectTransform.sizeDelta = Vector2.zero;
	}

	public void ExpandOut()
	{
		Player player = FindObjectOfType<Player>();
		if (player != null)
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(player.transform.position);
		}
		else
		{
			mask.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(Vector3.zero);
		}
		StartCoroutine(SizeChangingProcess(expandedRadius * Screen.width, expandSpeedRelativeToWidth * Screen.width, true));
	}

	void InstantExpand()
	{
		mask.rectTransform.sizeDelta = new Vector2(expandedRadius * Screen.width, expandedRadius * Screen.width);
	}

	IEnumerator SizeChangingProcess(float targetRadius, float changeSpeed, bool disableOnFinish)
	{
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
		}
	}
}
