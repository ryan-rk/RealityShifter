using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayBorders : MonoBehaviour
{
	[SerializeField] Color realColor;
	[SerializeField] Color notRealColor;
	[SerializeField] List<Image> bordersImages;

	// Start is called before the first frame update
	void Start()
	{
		UpdateBordersColor();
		if (RealityManager.Instance != null)
		{
			RealityManager.Instance.OnRealityShifted += UpdateBordersColor;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void UpdateBordersColor()
	{
		foreach (Image borderImage in bordersImages)
		{
			if (RealityManager.Instance != null)
			{
				borderImage.color = RealityManager.Instance.currentPlaneIsReal ? realColor : notRealColor;
			}
		}
	}
}
