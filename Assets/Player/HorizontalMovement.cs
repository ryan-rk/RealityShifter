using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
	[SerializeField] Player player;
	[Range(-1, 1)]
	public float normalizedMovement = 0f;
	// float prevNormalizedMovement = 0f;
	[SerializeField] float moveSpeed = 8f;
	[SerializeField] Animator spriteAC;
	[SerializeField] GameObject flipContainer;
	[SerializeField] ParticleSystem movementParticle;

	Rigidbody2D rb;
	GroundCheck groundCheck;
	public float speedControllability = 1f; // 1 for full control from movement script, 0 for fully no control
											// public bool isControllingVelocity = false;
	public bool isFacingRight { get; private set; } = true;

	private void Awake()
	{
		player.horizontalMovement = this;
	}

	private void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		groundCheck = GetComponent<GroundCheck>();
	}

	private void Update()
	{
		if (rb.velocity.x > 0.01)
		{
			FlipPlayer(true);
		}
		else if (rb.velocity.x < -0.01)
		{
			FlipPlayer(false);
		}

		if (groundCheck != null)
		{
			// if (groundCheck.isGrounded)
			// {
			if (Mathf.Abs(normalizedMovement) > 0 && Mathf.Abs(rb.velocity.x) > 0.01f)
			{
				SetMovementParticle(true);
			}
			// 	else
			// 	{
			// 		SetMovementParticle(false);
			// 	}
			// }
			else
			{
				SetMovementParticle(false);
			}
		}
	}

	private void FixedUpdate()
	{
		// if (!(normalizedMovement == 0 && prevNormalizedMovement == 0))
		// {
		// isControllingVelocity = true;
		// rb.velocity = new Vector2(speedModifier * moveSpeed * normalizedMovement, rb.velocity.y);
		float horizontalSpeed = Mathf.Lerp(rb.velocity.x, moveSpeed * normalizedMovement, speedControllability);
		rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
		// }
		// else
		// {
		// isControllingVelocity = false;
		// }
		// prevNormalizedMovement = normalizedMovement;
	}

	public void FlipPlayer(bool isFacingRight)
	{
		// flip body with parameter true if facing right, false if facing left
		float xScaling = Mathf.Abs(flipContainer.transform.localScale.x) * (isFacingRight ? 1 : -1);
		flipContainer.transform.localScale = new Vector2(xScaling, flipContainer.transform.localScale.y);
		this.isFacingRight = isFacingRight;
	}

	public void SetMovementParticle(bool isEnabled)
	{
		if (movementParticle != null)
		{
			if (isEnabled)
			{
				if (!movementParticle.isEmitting)
				{
					movementParticle.Play();
				}
			}
			else
			{
				if (movementParticle.isPlaying)
				{
					movementParticle.Stop();
				}
			}
		}
	}

	private void OnDisable()
	{
		normalizedMovement = 0;
		SetMovementParticle(false);
		rb.velocity = new Vector2(0, rb.velocity.y);
	}

}
