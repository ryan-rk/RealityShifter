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
		UpdateBackgroundReality();
		if (RealityManager.Instance != null)
		{
			RealityManager.Instance.OnRealityShifted += UpdateBackgroundReality;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void UpdateBackgroundReality()
	{
		if (RealityManager.Instance != null)
		{
			bool isReal = RealityManager.Instance.currentPlaneIsReal;
			bgColorSprite.color = isReal ? realColor : notRealColor;
			float yScaling = Mathf.Abs(bgImage.transform.localScale.y) * (isReal ? 1 : -1);
			bgImage.transform.localScale = new Vector2(bgImage.transform.localScale.x, yScaling);
		}
	}

	void RandomizeBgImagePosition()
	{
		float randomPosition = Random.Range(bgImageMinX, bgImageMaxX);
		bgImage.transform.position = new Vector2(randomPosition, bgImage.transform.position.y);
	}
}
