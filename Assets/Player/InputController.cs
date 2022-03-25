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
		player.horizontalMovement.normalizedMovement = Input.GetAxisRaw("Horizontal");
		if (Input.GetButtonDown("Jump"))
		{
			player.jumpController.Jump();
			player.wallGrabber.WallJump();
		}
		else if (Input.GetButtonUp("Jump"))
		{
			player.jumpController.StopJump();
		}
		if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
		{
			if (RealityManager.Instance != null)
			{
				RealityManager.Instance.ShiftRealityPlane();
			}
		}
	}

	void StopInputReceiver()
	{
		player.horizontalMovement.normalizedMovement = 0;
		this.enabled = false;
	}

	private void OnDisable()
	{
		player.OnPlayerDeath -= StopInputReceiver;
		player.OnPlayerWin -= StopInputReceiver;
	}
}
