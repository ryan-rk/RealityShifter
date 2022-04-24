using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSecret : MonoBehaviour
{
	[SerializeField] GameObject secretStage;
	[SerializeField] float keyPressWaitTime = 0.5f;

	private KeyCode[] secretKeys = new KeyCode[]
	{
		KeyCode.UpArrow,
		KeyCode.DownArrow,
		KeyCode.LeftArrow,
		KeyCode.RightArrow,
		KeyCode.R,
		KeyCode.K
	};

	float keypressTimer = 0f;
	int keypressIndex = 0;

	private void Update()
	{
		if (Input.GetKeyDown(secretKeys[keypressIndex]))
		{
			keypressIndex++;

			if (keypressIndex == secretKeys.Length)
			{
				EnableSecretStage();
				keypressTimer = 0f;
				keypressIndex = 0;
			}
			else
			{
				keypressTimer = keyPressWaitTime;
			}
		}
		else if (Input.anyKeyDown)
		{
			// Debug.Log("Wrong key sequence");
			keypressTimer = 0;
			keypressIndex = 0;
		}

		if (keypressTimer > 0)
		{
			keypressTimer -= Time.deltaTime;

			if (keypressTimer <= 0)
			{
				keypressTimer = 0;
				keypressIndex = 0;
			}
		}
	}

	void EnableSecretStage()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Glitch");
		}
		secretStage.SetActive(true);
		this.enabled = false;
	}
}
