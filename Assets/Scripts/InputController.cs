using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	[SerializeField] Player player;

	private void OnEnable()
	{
		player.OnPlayerDeath += StopInputReceiver;
		player.OnPlayerWin += StopInputReceiver;
	}

	// Update is called once per frame
	void Update()
	{
		player.horizontalMovement = Input.GetAxisRaw("Horizontal");
		if (Input.GetButtonDown("Jump"))
		{
			player.Jump();
		}
		if (Input.GetButtonDown("Fire1"))
		{
			RealityManager.Instance.ShiftRealityPlane();
		}
	}

	void StopInputReceiver()
	{
		this.enabled = false;
	}

	private void OnDisable()
	{
		player.OnPlayerDeath -= StopInputReceiver;
		player.OnPlayerWin -= StopInputReceiver;
	}
}
