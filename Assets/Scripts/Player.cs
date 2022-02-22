using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float moveSpeed = 8f;
	[SerializeField] float jumpForce = 20f;

	Rigidbody2D rb;
	GroundCheck groundCheck;

	public float horizontalMovement = 0f;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		groundCheck = GetComponent<GroundCheck>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void Jump()
	{
		if (groundCheck.isGrounded)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(moveSpeed * horizontalMovement, rb.velocity.y);
	}
}
