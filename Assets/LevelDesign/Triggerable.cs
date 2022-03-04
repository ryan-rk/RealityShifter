using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour
{
	public bool isTriggering { get; private set; } = false;
	[SerializeField] Transform controlLineAttacher;
	public event Action OnObjectTriggered;
	public event Action OnObjectStopTriggered;

	public void Trigger()
	{
		if (isTriggering) { return; }
		isTriggering = true;
		OnObjectTriggered?.Invoke();
	}

	public void StopTrigger()
	{
		if (!isTriggering) { return; }
		isTriggering = false;
		OnObjectStopTriggered?.Invoke();
	}

	public Vector2 GetControlLineAttachPoint()
	{
		return controlLineAttacher.position;
	}
}
