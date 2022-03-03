using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	[SerializeField] Color realColor;
	[SerializeField] Color notRealColor;
	[SerializeField] SpriteRenderer bgColorSprite;
	[SerializeField] SpriteRenderer bgImage;
	[SerializeField] float bgImageMinX;
	[SerializeField] float bgImageMaxX;

	// Start is called before the first frame update
	void Start()
	{
		RandomizeBgImagePosition();
		UpdateBackgroundColor();
		if (RealityManager.Instance != null)
		{
			RealityManager.Instance.OnRealityShifted += UpdateBackgroundColor;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void UpdateBackgroundColor()
	{
		if (RealityManager.Instance != null)
		{
			bgColorSprite.color = RealityManager.Instance.currentPlaneIsReal ? realColor : notRealColor;
		}
	}

	void RandomizeBgImagePosition()
	{
		float randomPosition = Random.Range(bgImageMinX, bgImageMaxX);
		bgImage.transform.position = new Vector2(randomPosition, bgImage.transform.position.y);
	}
}
