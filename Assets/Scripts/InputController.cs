using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
		{
			RealityManager.Instance.ShiftRealityPlane();
		}
	}

	void StopInputReceiver()
	{
		player.horizontalMovement = 0;
		this.enabled = false;
	}

	private void OnDisable()
	{
		player.OnPlayerDeath -= StopInputReceiver;
		player.OnPlayerWin -= StopInputReceiver;
	}
}
