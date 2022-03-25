using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
	[SerializeField] Player player;
	[SerializeField] float jumpForce = 15f;
	[SerializeField] float shortJumpVelocityScale = 0.5f;
	[SerializeField] float coyoteTime = 0.1f;
	float remainingCoyote = 0;

	Rigidbody2D rb;
	GroundCheck groundCheck;

	private void Awake()
	{
		player.jumpController = this;
	}

	private void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		groundCheck = GetComponent<GroundCheck>();
	}

	// Update is called once per frame
	void Update()
	{
		if (groundCheck.isGrounded)
		{
			// Coyote time management
			remainingCoyote = coyoteTime;
		}
		else
		{
			remainingCoyote -= Time.deltaTime;
		}
	}

	public void ExecuteJump(Vector2 direction)
	{
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.AddForce(direction.normalized * jumpForce, ForceMode2D.Impulse);
		// rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		// if (AudioManager.Instance != null)
		// {
		// 	AudioManager.Instance.PlaySound("Jump");
		// }
	}

	public void Jump()
	{
		if (remainingCoyote > 0 && this.enabled)
		{
			remainingCoyote = 0;
			ExecuteJump(new Vector2(0, 1));
		}
	}

	public void StopJump()
	{
		if (rb.velocity.y > 0 && this.enabled)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * shortJumpVelocityScale);
		}
	}

}
