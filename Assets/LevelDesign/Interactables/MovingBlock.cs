using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Triggerable))]
public class MovingBlock : MonoBehaviour
{
	[SerializeField] SpriteRenderer activeSprite;
	[SerializeField] SpriteRenderer inActiveSprite;
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float initialOffset;
	// [SerializeField] float activatedVelocity = 1f;
	// [SerializeField] float returnVelocity = 1f;
	[SerializeField] Vector2 endPositionOffset;
	[SerializeField] LayerMask boxCastLayerMask;
	[SerializeField] LineRenderer railLine;
	[SerializeField] Gradient railLineActiveColor;
	[SerializeField] Gradient railLineInactiveColor;
	[SerializeField] GameObject railLineStartPoint;
	[SerializeField] GameObject railLineEndPoint;
	[SerializeField] PlayerFeetAttacher playerFeetAttacher;

	Rigidbody2D rb;
	BoxCollider2D boxCol;
	Triggerable triggerable;
	Vector2 startPosition;
	RaycastHit2D boxCastHit;
	bool isMoveForward = true;
	// bool hasFirstFixedUpdate = false;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		boxCol = GetComponent<BoxCollider2D>();
		triggerable = GetComponent<Triggerable>();
		startPosition = transform.position;
		SetSprite(triggerable.isTriggering);
		ConfigureRailLine();
		if (initialOffset > 1)
		{
			initialOffset = 2 - initialOffset;
			isMoveForward = false;
		}
		transform.position = (Vector2)transform.position + initialOffset * endPositionOffset;
	}

	// private void Update()
	void FixedUpdate()
	{
		if (triggerable.isTriggering)
		{
			SetSprite(true);
			railLine.colorGradient = railLineActiveColor;
			MoveBlock();
			// if ((Vector2)transform.position != endPositionOffset + startPosition)
			// {
			// MoveBlock(true);
			// }
		}
		else
		{
			SetSprite(false);
			railLine.colorGradient = railLineInactiveColor;
			// if ((Vector2)transform.position != startPosition)
			// {
			// MoveBlock(false);
			// }
		}
		// if (!hasFirstFixedUpdate)
		// {
		// 	hasFirstFixedUpdate = true;
		// }
	}

	void SetSprite(bool isActive)
	{
		activeSprite.gameObject.SetActive(isActive);
		inActiveSprite.gameObject.SetActive(!isActive);
	}

	void MoveBlock()
	{
		Vector2 targetPosition = isMoveForward ? startPosition + endPositionOffset : startPosition;
		if ((Vector2)transform.position != targetPosition)
		{
			MoveBlockTowards(targetPosition);
		}
		else
		{
			isMoveForward = !isMoveForward;
		}
		// Vector2 newPos = Vector2.MoveTowards(transform.position, isTriggered ? activatedMovementOffset + nonActivatedPosition : nonActivatedPosition, Time.deltaTime * (isTriggered ? activatedVelocity : returnVelocity));
	}

	void MoveBlockTowards(Vector2 destination)
	{
		Vector2 newPos = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
		Vector2 moveDirection = newPos - (Vector2)transform.position;
		boxCastHit = Physics2D.BoxCast(transform.position, boxCol.size, 0, moveDirection, moveDirection.magnitude, boxCastLayerMask);
		if (!boxCastHit)
		{
			playerFeetAttacher.MoveOverlapObjects(moveDirection);
			rb.MovePosition(newPos);
			// transform.position = newPos;
		}
		else
		{
			// Debug.Log("Cast: " + boxCol.size + " direction: " + moveDirection + " magnitude: " + moveDirection.magnitude);
			// Debug.Log("box cast hit: " + gameObject.transform.parent.transform.parent + " with " + boxCastHit.collider.gameObject.transform.parent.transform.parent + " at point: " + boxCastHit.point);
		}
	}

	void ConfigureRailLine()
	{
		railLine.SetPosition(0, startPosition);
		railLine.SetPosition(1, startPosition + endPositionOffset);
		railLineStartPoint.transform.position = startPosition;
		railLineEndPoint.transform.position = startPosition + endPositionOffset;
	}

}
