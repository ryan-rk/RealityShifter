using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	public event Action OnPlayerReachedExit;

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
		if (other.TryGetComponent<Player>(out Player player))
		{
			player.SetWin();
			OnPlayerReachedExit?.Invoke();
		}
	}
}
