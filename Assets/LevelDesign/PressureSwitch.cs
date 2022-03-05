using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
	[SerializeField] Triggerable controlledTriggerable;
	[SerializeField] GameObject switchSprites;
	float pressedPositionY = 0.15f;
	float releasedPositionY = 0.35f;
	float switchMoveSpeed = 1f;
	[SerializeField] SpriteRenderer switchOnSprite;
	[SerializeField] SpriteRenderer switchOffSprite;
	[SerializeField] SpriteRenderer baseSprite;
	int triggerCount = 0;

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

	[SerializeField] List<string> excludedPressureDetectionTag;

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
		baseSprite.color = spriteColor;
		if (groundBody != null)
		{
			groundBody.SetActive(isReal);
		}
		gameObject.layer = isReal ? realLayerIndex : notRealLayerIndex;
		switchBody.layer = isReal ? realLayerIndex : notRealLayerIndex;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// if (excludedPressureDetectionTag.Contains(other.gameObject.tag) || gameObject.layer == notRealLayerIndex)
		if (excludedPressureDetectionTag.Contains(other.gameObject.tag) || other.attachedRigidbody.bodyType != RigidbodyType2D.Dynamic)
		{
			return;
		}
		triggerCount += 1;
		if (triggerCount == 1)
		{
			SetSwitchPressed(true);
		}
	}

	// private void OnTriggerStay2D(Collider2D other)
	// {
	// 	controlledTriggerable.Trigger();
	// 	SetSwitchPressed(true);
	// }

	private void OnTriggerExit2D(Collider2D other)
	{
		// if (excludedPressureDetectionTag.Contains(other.gameObject.tag) || gameObject.layer == notRealLayerIndex)
		if (excludedPressureDetectionTag.Contains(other.gameObject.tag) || other.attachedRigidbody.bodyType != RigidbodyType2D.Dynamic)
		{
			return;
		}
		triggerCount -= 1;
		if (triggerCount <= 0)
		{
			SetSwitchPressed(false);
		}
	}

	void SetSwitchPressed(bool isOn)
	{
		StartCoroutine(SwitchPressedProcess(isOn));
	}

	IEnumerator SwitchPressedProcess(bool isPressed)
	{
		if (!isPressed)
		{
			switchOnSprite.gameObject.SetActive(false);
			switchOffSprite.gameObject.SetActive(true);
			controlLine.colorGradient = lineInactiveColor;
			controlledTriggerable?.StopTrigger();
		}
		while (switchSprites.transform.localPosition.y != (isPressed ? pressedPositionY : releasedPositionY))
		{
			float newPositionY = Mathf.MoveTowards(switchSprites.transform.localPosition.y, isPressed ? pressedPositionY : releasedPositionY, switchMoveSpeed * Time.deltaTime);
			switchSprites.transform.localPosition = new Vector2(switchSprites.transform.localPosition.x, newPositionY);
			yield return null;
		}
		if (isPressed)
		{
			switchOnSprite.gameObject.SetActive(true);
			switchOffSprite.gameObject.SetActive(false);
			controlLine.colorGradient = lineActiveColor;
			controlledTriggerable?.Trigger();
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
	}
}
