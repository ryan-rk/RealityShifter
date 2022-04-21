using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityGenerator : MonoBehaviour
{
	[SerializeField] RealityState realityState;
	[SerializeField] Triggerable controlledTriggerable;
	[SerializeField] LineRenderer controlLine;
	[SerializeField] Transform controlLineEnd;
	[SerializeField] bool isInvertLineDirection;
	[SerializeField] Gradient lineActiveColor;
	[SerializeField] Gradient lineInactiveColor;
	[SerializeField] ParticleSystem electricSpark;
	[SerializeField] Color activeSparkColor;
	[SerializeField] Color inactiveSparkColor;

	ParticleSystem.TrailModule sparkTrail;

	// Start is called before the first frame update
	void Start()
	{
		ConfigureControlLine();
	}

	private void OnEnable()
	{
		sparkTrail = electricSpark.trails;
		realityState.OnStateUpdated += SetGeneratorState;
	}

	private void OnDisable()
	{
		realityState.OnStateUpdated -= SetGeneratorState;
	}

	void SetGeneratorState(bool isOn)
	{
		if (isOn)
		{
			controlLine.colorGradient = lineActiveColor;
			controlledTriggerable?.Trigger();
			if (electricSpark.isPaused)
			{
				electricSpark.Play();
				sparkTrail.colorOverLifetime = activeSparkColor;
			}
		}
		else
		{
			controlLine.colorGradient = lineInactiveColor;
			controlledTriggerable?.StopTrigger();
			if (electricSpark.isPlaying)
			{
				electricSpark.Pause();
				sparkTrail.colorOverLifetime = inactiveSparkColor;
			}
		}
	}

	void ConfigureControlLine()
	{
		if (controlledTriggerable == null)
		{
			return;
		}
		if (controlledTriggerable.GetControlLineAttachPoint().x == transform.position.x || controlledTriggerable.GetControlLineAttachPoint().y == transform.position.y)
		{
			controlLine.positionCount = 2;
		}
		else
		{
			controlLine.positionCount = 3;
			Vector2 middlePoint;
			if (!isInvertLineDirection)
			{
				middlePoint = new Vector2(controlledTriggerable.GetControlLineAttachPoint().x, transform.position.y);
			}
			else
			{
				middlePoint = new Vector2(transform.position.x, controlledTriggerable.GetControlLineAttachPoint().y);
			}
			controlLine.SetPosition(1, middlePoint);
		}
		controlLine.SetPosition(0, transform.position);
		controlLine.SetPosition(controlLine.positionCount - 1, controlledTriggerable.GetControlLineAttachPoint());
		controlLineEnd.position = controlledTriggerable.GetControlLineAttachPoint();
	}
}
