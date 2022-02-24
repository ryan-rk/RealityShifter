using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRealDetector : MonoBehaviour
{
	[SerializeField] Player player;
	public bool isOnNotReal { get; private set; } = false;
	[SerializeField] List<string> excludedTrappedTag;

	// Start is called before the first frame update
	void Start()
	{
		RealityManager.Instance.OnRealityShifted += CheckTrappedShifting;
	}

	// Update is called once per frame
	void Update()
	{
	}

	void CheckTrappedShifting()
	{
		if (isOnNotReal)
		{
			player.SetDeath();
		}
	}

	private void OnDisable()
	{
		RealityManager.Instance.OnRealityShifted -= CheckTrappedShifting;
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
