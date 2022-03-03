using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float moveSpeed = 8f;
	[SerializeField] float jumpForce = 20f;
	[SerializeField] float shortJumpVelocityScale = 0.5f;
	public float horizontalMovement = 0f;

	[SerializeField] GameObject playerSprite;
	Animator spriteAnimator;
	[SerializeField] float deathShrinkSpeed = 0.1f;
	[SerializeField] ParticleSystem deathParticle;
	[SerializeField] float deathShakeIntensity = 10f;
	[SerializeField] float deathShakeDuration = 0.5f;

	[SerializeField] ParticleSystem winParticle;

	Rigidbody2D rb;
	Collider2D col;
	[HideInInspector] public GroundCheck groundCheck { get; private set; }

	enum PlayerState
	{
		Default, Death, Win
	}
	PlayerState currentState = PlayerState.Default;

	public event Action OnPlayerDeath;
	public event Action OnPlayerWin;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		groundCheck = GetComponent<GroundCheck>();

		spriteAnimator = playerSprite.GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
	{
		if (rb.velocity.x > 0.01)
		{
			FlipSprite(true);
		}
		else if (rb.velocity.x < -0.01)
		{
			FlipSprite(false);
		}
		if (groundCheck.isGrounded)
		{
			if (RealityManager.Instance != null)
			{
				RealityManager.Instance.RecoverShift();
			}
		}
	}

	public void Spawn(Vector2 spawnPoint)
	{
		transform.position = spawnPoint;
	}

	public void Jump()
	{
		if (groundCheck.isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.PlaySound("Jump");
			}
		}
	}

	public void StopJump()
	{
		if (rb.velocity.y > 0)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * shortJumpVelocityScale);
		}
	}

	public void FlipSprite(bool isFacingRight)
	{
		// flip body with parameter true if facing right, false if facing left
		float xScaling = Mathf.Abs(playerSprite.transform.localScale.x) * (isFacingRight ? 1 : -1);
		playerSprite.transform.localScale = new Vector2(xScaling, playerSprite.transform.localScale.y);
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(moveSpeed * horizontalMovement, rb.velocity.y);
	}

	public void SetDeath()
	{
		if (currentState == PlayerState.Death)
		{
			return;
		}
		currentState = PlayerState.Death;
		horizontalMovement = 0;
		rb.velocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Kinematic;
		col.enabled = false;
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Hit");
			AudioManager.Instance.PlaySound("Death");
		}
		spriteAnimator.Play("Death");
		ScreenShaker mainVcamScreenShaker = CameraManager.Instance.mainVcamScreenShaker;
		if (mainVcamScreenShaker != null)
		{
			mainVcamScreenShaker.ScreenShake(deathShakeIntensity, deathShakeDuration);
		}
		StartCoroutine(EndDeathAnimation());
		OnPlayerDeath?.Invoke();
	}

	IEnumerator EndDeathAnimation()
	{
		while (Mathf.Abs(playerSprite.transform.localScale.x) > 0)
		{
			yield return null;
			float shrinkedScale = Mathf.MoveTowards(playerSprite.transform.localScale.x, 0, deathShrinkSpeed * Time.deltaTime);
			playerSprite.transform.localScale = new Vector2(shrinkedScale, shrinkedScale);
		}
		deathParticle.Play();
		GameManager.Instance.ManagePlayerDeath();
	}

	public void SetWin()
	{
		if (currentState == PlayerState.Win)
		{
			return;
		}
		currentState = PlayerState.Win;
		horizontalMovement = 0;
		rb.velocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Kinematic;
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Win");
			AudioManager.Instance.PlaySound("Teleport");
		}
		spriteAnimator.Play("Win");
		winParticle.Play();
		OnPlayerWin?.Invoke();
	}

}
