using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDetector : MonoBehaviour
{
	[SerializeField] List<string> excludedHazardTag;
	public event Action OnHazardDetected;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Hazard>())
		{
			if (!excludedHazardTag.Contains(other.gameObject.tag))
			{
				OnHazardDetected?.Invoke();
			}
		}
	}
}
