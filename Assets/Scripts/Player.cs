using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Vector2 spawnPoint;
	[SerializeField] bool isDrawSpawnPoint;
	[SerializeField] float moveSpeed = 8f;
	[SerializeField] float jumpForce = 20f;
	public float horizontalMovement = 0f;

	[SerializeField] GameObject playerSprite;
	Animator spriteAnimator;
	[SerializeField] float deathShrinkSpeed = 0.1f;
	[SerializeField] ParticleSystem deathParticle;
	[SerializeField] float deathShakeIntensity = 10f;
	[SerializeField] float deathShakeDuration = 0.5f;

	Rigidbody2D rb;
	Collider2D col;
	GroundCheck groundCheck;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		groundCheck = GetComponent<GroundCheck>();

		spriteAnimator = playerSprite.GetComponent<Animator>();

		Spawn();
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
	}

	void Spawn()
	{
		rb.velocity = Vector2.zero;
		transform.position = spawnPoint;
	}

	public void Jump()
	{
		if (groundCheck.isGrounded)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
		rb.velocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Kinematic;
		col.enabled = false;
		spriteAnimator.Play("Death");
		ScreenShaker mainVcamScreenShaker = CameraManager.Instance.mainVcamScreenShaker;
		if (mainVcamScreenShaker != null)
		{
			mainVcamScreenShaker.ScreenShake(deathShakeIntensity, deathShakeDuration);
		}
		StartCoroutine(EndDeathAnimation());
	}

	IEnumerator EndDeathAnimation()
	{
		while (playerSprite.transform.localScale.x > 0)
		{
			yield return null;
			float shrinkedScale = Mathf.MoveTowards(playerSprite.transform.localScale.x, 0, deathShrinkSpeed * Time.deltaTime);
			playerSprite.transform.localScale = new Vector2(shrinkedScale, shrinkedScale);
		}
		deathParticle.Play();
		GameManager.Instance.ManagePlayerDeath();
	}

	private void OnDrawGizmos()
	{
		if (isDrawSpawnPoint)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawIcon(spawnPoint, "Record Off", true);
			UnityEditor.EditorGUIUtility.IconContent("Record Off");
		}
	}
}
