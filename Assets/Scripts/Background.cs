using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	[SerializeField] Color realColor;
	[SerializeField] Color notRealColor;
	SpriteRenderer bgSprite;

	// Start is called before the first frame update
	void Start()
	{
		bgSprite = GetComponent<SpriteRenderer>();
		UpdateBackgroundColor();
		RealityManager.Instance.OnRealityShifted += UpdateBackgroundColor;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void UpdateBackgroundColor()
	{
		bgSprite.color = RealityManager.Instance.currentPlaneIsReal ? realColor : notRealColor;
	}
}
