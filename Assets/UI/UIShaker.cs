using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShaker : MonoBehaviour
{
	RectTransform rectTransform;

	// Start is called before the first frame update
	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UIShake(float intensity, float duration, float frequency)
	{
		StartCoroutine(UIShakeProcess(intensity, duration, frequency));
	}

	IEnumerator UIShakeProcess(float intensity, float duration, float frequency)
	{
		float remainingDuration = duration;
		Vector2 originalPosition = rectTransform.anchoredPosition;
		while (remainingDuration > 0)
		{
			float scaledIntensity = intensity * rectTransform.sizeDelta.y;
			Vector2 randomOffset = new Vector2(Random.Range(-scaledIntensity, scaledIntensity), Random.Range(-scaledIntensity, scaledIntensity));
			rectTransform.anchoredPosition = originalPosition + randomOffset;
			yield return new WaitForSeconds(1 / frequency);
			remainingDuration -= 1 / frequency;
		}
		rectTransform.anchoredPosition = originalPosition;
		Debug.Log("Shaking ended");
	}
}
