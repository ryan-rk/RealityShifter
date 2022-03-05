using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRealDetector : MonoBehaviour
{
	public bool isOnNotReal { get; private set; } = false;
	[SerializeField] List<string> excludedTrappedTag;
	public event Action OnTrappedShifting;

	// Start is called before the first frame update
	void Start()
	{
		if (RealityManager.Instance != null)
		{
			RealityManager.Instance.OnRealityShifted += CheckTrappedShifting;
		}
	}

	// Update is called once per frame
	void Update()
	{
	}

	void CheckTrappedShifting()
	{
		if (isOnNotReal)
		{
			// player.SetDeath();
			OnTrappedShifting?.Invoke();
		}
	}

	private void OnDisable()
	{
		if (RealityManager.Instance != null)
		{
			RealityManager.Instance.OnRealityShifted -= CheckTrappedShifting;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!excludedTrappedTag.Contains(other.gameObject.tag))
		{
			isOnNotReal = true;
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (!excludedTrappedTag.Contains(other.gameObject.tag))
		{
			isOnNotReal = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (!excludedTrappedTag.Contains(other.gameObject.tag))
		{
			isOnNotReal = false;
		}
	}
}
