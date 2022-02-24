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
		RealityManager.Instance.OnRealityShifted += UpdateBordersColor;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void UpdateBordersColor()
	{
		foreach (Image borderImage in bordersImages)
		{
			borderImage.color = RealityManager.Instance.currentPlaneIsReal ? realColor : notRealColor;
		}
	}
}
