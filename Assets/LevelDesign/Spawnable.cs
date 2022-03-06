using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
	public event Action OnSpawned;
	public event Action OnSpawnedTargetDestroyed;

	// Start is called before the first frame update
	void Start()
	{
		OnSpawned?.Invoke();
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Spawned");
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnDestroy()
	{
		Debug.Log("target destroyed");
		OnSpawnedTargetDestroyed?.Invoke();
	}
}
