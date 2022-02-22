using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityManager : MonoBehaviour
{
	public static RealityManager Instance;

	public List<RealityState> managedRealityObjects { get; private set; } = new List<RealityState>();

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			Debug.Log("Managed list count: " + managedRealityObjects.Count);
		}
	}

	public void AddToManaged(RealityState realityObject)
	{
		managedRealityObjects.Add(realityObject);
	}

	public void RemoveFromManaged(RealityState realityObject)
	{
		managedRealityObjects.Remove(realityObject);
	}
}
