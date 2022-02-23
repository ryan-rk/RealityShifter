using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactTrigger : MonoBehaviour
{
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
		if (other.TryGetComponent<RealityState>(out RealityState realityObject))
		{
			realityObject.SetRealityShiftable(false);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.TryGetComponent<RealityState>(out RealityState realityObject))
		{
			realityObject.SetRealityShiftable(true);
		}
	}
}
