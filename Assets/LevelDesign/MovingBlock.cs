using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Triggerable))]
public class MovingBlock : MonoBehaviour
{
	[SerializeField] SpriteRenderer activeSprite;
	[SerializeField] SpriteRenderer inActiveSprite;
	[SerializeField] float activatedVelocity = 1f;
	[SerializeField] float returnVelocity = 1f;
	[SerializeField] Vector2 activatedMovementOffset;
	[SerializeField] LayerMask boxCastLayerMask;

	Rigidbody2D rb;
	BoxCollider2D boxCol;
	Triggerable triggerable;
	Vector2 nonActivatedPosition;
	RaycastHit2D boxCastHit;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		boxCol = GetComponent<BoxCollider2D>();
		triggerable = GetComponent<Triggerable>();
		nonActivatedPosition = transform.position;
		SetSprite(triggerable.isTriggering);
	}

	void FixedUpdate()
	{
		if (triggerable.isTriggering)
		{
			SetSprite(true);
			if ((Vector2)transform.position != activatedMovementOffset + nonActivatedPosition)
			{
				MoveBlock(true);
			}
		}
		else if (!triggerable.isTriggering)
		{
			SetSprite(false);
			if ((Vector2)transform.position != nonActivatedPosition)
			{
				MoveBlock(false);
			}
		}
	}

	void SetSprite(bool isActive)
	{
		activeSprite.gameObject.SetActive(isActive);
		inActiveSprite.gameObject.SetActive(!isActive);
	}

	void MoveBlock(bool isTriggered)
	{
		Vector2 newPos = Vector2.MoveTowards(transform.position, isTriggered ? activatedMovementOffset + nonActivatedPosition : nonActivatedPosition, Time.deltaTime * (isTriggered ? activatedVelocity : returnVelocity));
		Vector2 moveDirection = newPos - (Vector2)transform.position;
		boxCastHit = Physics2D.BoxCast(transform.position, boxCol.size, 0, moveDirection, moveDirection.magnitude, boxCastLayerMask);
		if (!boxCastHit)
		{
			rb.MovePosition(newPos);
		}
		else
		{
			Debug.Log("box cast hit");
		}
	}

}
