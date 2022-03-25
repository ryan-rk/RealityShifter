using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabber : MonoBehaviour
{
	[SerializeField] Player player;
	[SerializeField] Transform wallChecker;
	[SerializeField] float wallCheckRadius = 0.2f;
	[SerializeField] float slideSpeed;
	[SerializeField] bool canWallJump = false;
	[SerializeField] Vector2 wallJumpDirection = new Vector2(1f, 1f);
	[SerializeField] float wallSlidabilityReductionRate = 0.8f;
	[SerializeField] float regainControlDelay = 1f;
	[SerializeField] float wallJumpLerpPower = 3f;
	[SerializeField] LayerMask groundLayer;
	[SerializeField] GameObject bottomContactTrigger;
	[SerializeField] GameObject wallContactTrigger;

	Rigidbody2D rb;
	bool isTouchWall;
	bool isWallSliding;
	float wallSlidability = 1f;
	// bool isWallJumping;
	Vector2 scaledWallJumpDirection;
	// WaitForSeconds regainControlWait;

	private void Awake()
	{
		player.wallGrabber = this;
	}

	private void OnEnable()
	{
		rb = GetComponent<Rigidbody2D>();
		// regainControlWait = new WaitForSeconds(regainControlDelay);
	}

	private void Update()
	{
		isTouchWall = Physics2D.OverlapCircle(wallChecker.position, wallCheckRadius, groundLayer);

		if (isTouchWall && !player.groundCheck.isGrounded && player.horizontalMovement.normalizedMovement != 0)
		{
			isWallSliding = true;
			wallContactTrigger.SetActive(true);
			bottomContactTrigger.SetActive(false);
		}
		else
		{
			if (isWallSliding)
			{
				isWallSliding = false;
				wallContactTrigger.SetActive(false);
				bottomContactTrigger.SetActive(true);
			}
		}
		if (player.groundCheck.isGrounded)
		{
			wallSlidability = 1f;
		}
	}

	private void FixedUpdate()
	{
		if (isWallSliding)
		{
			float scaledSlideSpeed = Mathf.Lerp(Mathf.Abs(rb.velocity.y), slideSpeed, wallSlidability);
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -scaledSlideSpeed));
		}
		// if (isWallJumping)
		// {
		// 	rb.velocity = new Vector2(directionalWallJumpForce.normalized.x * wallJumpHorizontalSpeed, rb.velocity.y);
		// }
	}

	public void WallJump()
	{
		// if (isWallSliding && !isWallJumping)
		if (canWallJump && isWallSliding)
		{
			// isWallJumping = true;
			scaledWallJumpDirection = new Vector2(-player.horizontalMovement.normalizedMovement * wallJumpDirection.x, wallJumpDirection.y);
			// horizontalMovement.enabled = false;
			// rb.AddForce(new Vector2(0, wallJumpForce.y), ForceMode2D.Impulse);
			player.jumpController.ExecuteJump(scaledWallJumpDirection);
			wallSlidability *= wallSlidabilityReductionRate;
			// RegainControl();
			StopAllCoroutines();
			StartCoroutine(RegainControlProcess());
		}
	}

	IEnumerator RegainControlProcess()
	{
		float remainingDuration = regainControlDelay;
		while (remainingDuration > 0)
		{
			player.horizontalMovement.speedControllability = Mathf.Lerp(0, 1, Mathf.Pow((regainControlDelay - remainingDuration) / regainControlDelay, wallJumpLerpPower));
			remainingDuration -= Time.deltaTime;
			yield return null;
		}
		RegainControl();
	}

	void RegainControl()
	{
		player.horizontalMovement.speedControllability = 1;
		// horizontalMovement.enabled = true;
	}

	private void OnDisable()
	{
		RegainControl();
	}
}
