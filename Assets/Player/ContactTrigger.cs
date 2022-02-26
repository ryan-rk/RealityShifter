using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactTrigger : MonoBehaviour
{
	[SerializeField] List<string> excludedContactTag;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("trigger enter");
		if (other.TryGetComponent<RealityState>(out RealityState realityObject))
		{
			if (!excludedContactTag.Contains(other.gameObject.tag))
			{
				realityObject.SetContacted(true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("trigger exit");
		if (other.TryGetComponent<RealityState>(out RealityState realityObject))
		{
			if (!excludedContactTag.Contains(other.gameObject.tag))
			{
				realityObject.SetContacted(false);
			}
		}
	}
}
