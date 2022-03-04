using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
	[SerializeField] Triggerable controlledTriggerable;
	[SerializeField] SpriteRenderer switchOnSprite;
	[SerializeField] SpriteRenderer switchOffSprite;
	[SerializeField] LineRenderer controlLine;
	[SerializeField] bool isInvertLineDirection;
	[SerializeField] Gradient lineActiveColor;
	[SerializeField] Gradient lineInactiveColor;

	[SerializeField] RealityState attachedRealityObject;
	[SerializeField] float realAlpha = 1f;
	[SerializeField] float notRealAlpha = 0.7f;
	[SerializeField] GameObject groundBody;
	[SerializeField] GameObject switchBody;
	int realLayerIndex = 7;
	int notRealLayerIndex = 8;

	// Start is called before the first frame update
	void Start()
	{
		ConfigureControlLine();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnEnable()
	{
		if (attachedRealityObject != null)
		{
			attachedRealityObject.OnStateUpdated += UpdateRealityState;
		}
	}

	private void OnDisable()
	{
		if (attachedRealityObject != null)
		{
			attachedRealityObject.OnStateUpdated -= UpdateRealityState;
		}
	}

	void UpdateRealityState(bool isReal)
	{
		Color spriteColor = switchOnSprite.color;
		spriteColor.a = isReal ? realAlpha : notRealAlpha;
		switchOnSprite.color = spriteColor;
		switchOffSprite.color = spriteColor;
		if (groundBody != null)
		{
			groundBody.SetActive(isReal);
		}
		gameObject.layer = isReal ? realLayerIndex : notRealLayerIndex;
		switchBody.layer = isReal ? realLayerIndex : notRealLayerIndex;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		controlledTriggerable.Trigger();
		SetTriggeredChanges(true);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		controlledTriggerable.Trigger();
		SetTriggeredChanges(true);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		controlledTriggerable.StopTrigger();
		SetTriggeredChanges(false);
	}

	void SetTriggeredChanges(bool isOn)
	{
		switchOnSprite.gameObject.SetActive(isOn);
		switchOffSprite.gameObject.SetActive(!isOn);
		controlLine.colorGradient = isOn ? lineActiveColor : lineInactiveColor;
	}

	void ConfigureControlLine()
	{
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
	}
}
