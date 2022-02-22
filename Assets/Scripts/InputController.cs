using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	[SerializeField] Player player;

	// Update is called once per frame
	void Update()
	{
		player.horizontalMovement = Input.GetAxisRaw("Horizontal");
		if (Input.GetButtonDown("Jump"))
		{
			player.Jump();
		}
	}
}
