using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Vector2 spawnPoint;
	[SerializeField] bool isDrawSpawnPoint;
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

		Spawn();
	}

	// Update is called once per frame
	void Update()
	{
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

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(moveSpeed * horizontalMovement, rb.velocity.y);
	}

	public void SetDeath()
	{
		Debug.Log("Player is dead, respawning...");
		Spawn();
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
