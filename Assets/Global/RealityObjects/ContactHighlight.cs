using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactHighlight : MonoBehaviour
{
	[SerializeField] Color highlightColor = Color.white;
	[SerializeField] float hightlightWidth = 0.5f;
	PolygonCollider2D pCol;
	LineRenderer lineRenderer;

	private void Start()
	{
		pCol = GetComponent<PolygonCollider2D>();
		lineRenderer = GetComponent<LineRenderer>();
		ConfigureHighlight();
	}

	void ConfigureHighlight()
	{
		Vector2[] pColPoints = pCol.points;

		lineRenderer.startColor = highlightColor;
		lineRenderer.endColor = highlightColor;
		lineRenderer.startWidth = hightlightWidth;

		for (int i = 0; i < pColPoints.Length; i++)
		{
			pColPoints[i] = pCol.transform.TransformPoint(pColPoints[i]);
		}

		lineRenderer.positionCount = pColPoints.Length + 1;
		for (int i = 0; i < pColPoints.Length; i++)
		{
			lineRenderer.SetPosition(i, pColPoints[i]);

			if (i == (pColPoints.Length - 1))
			{
				lineRenderer.SetPosition(pColPoints.Length, pColPoints[0]);
			}
		}
	}

}
