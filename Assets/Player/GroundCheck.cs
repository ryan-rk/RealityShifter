using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
	[SerializeField] Player player;
	public bool isGrounded { get; private set; } = false;
	[SerializeField] LayerMask groundLayer;
	[SerializeField] Vector3 boxCastOffset = Vector3.zero;
	[SerializeField] Vector3 boxCastSize = new Vector2(1, 1);
	[SerializeField] float boxCastDistance = 1f;

	[SerializeField] bool isDrawLines = false;

	bool prevFrameIsGrounded = false;
	public event Action OnLandedGround;

	private void Awake()
	{
		player.groundCheck = this;
	}

	// Update is called once per frame
	void Update()
	{
		isGrounded = Physics2D.BoxCast(transform.position + boxCastOffset, boxCastSize, 0, Vector2.down, boxCastDistance, groundLayer);
		if (prevFrameIsGrounded != isGrounded)
		{
			OnLandedGround?.Invoke();
		}
		prevFrameIsGrounded = isGrounded;
	}

	private void OnDrawGizmos()
	{
		if (isDrawLines)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(transform.position + boxCastOffset - new Vector3(boxCastSize.x / 2, boxCastSize.y / 2, 0), transform.position + boxCastOffset + new Vector3(boxCastSize.x / 2, -boxCastSize.y / 2, 0));
			Gizmos.DrawLine(transform.position + boxCastOffset + new Vector3(boxCastSize.x / 2, -boxCastSize.y / 2, 0), transform.position + boxCastOffset + new Vector3(boxCastSize.x / 2, boxCastSize.y / 2, 0));
			Gizmos.DrawLine(transform.position + boxCastOffset + new Vector3(boxCastSize.x / 2, boxCastSize.y / 2, 0), transform.position + boxCastOffset - new Vector3(boxCastSize.x / 2, -boxCastSize.y / 2, 0));
			Gizmos.DrawLine(transform.position + boxCastOffset - new Vector3(boxCastSize.x / 2, -boxCastSize.y / 2, 0), transform.position + boxCastOffset - new Vector3(boxCastSize.x / 2, boxCastSize.y / 2, 0));

			if (isGrounded)
			{
				Gizmos.color = Color.green;
			}
			else
			{
				Gizmos.color = Color.red;
			}

			// Gizmos.DrawLine(transform.position + raycastOffset, transform.position + raycastOffset + Vector3.down * raycastLength);
			Gizmos.DrawLine(transform.position + boxCastOffset - new Vector3(boxCastSize.x / 2, boxCastSize.y / 2, 0), transform.position + boxCastOffset - new Vector3(boxCastSize.x / 2, boxCastSize.y / 2, 0) - new Vector3(0, boxCastDistance, 0));
			Gizmos.DrawLine(transform.position + boxCastOffset + new Vector3(boxCastSize.x / 2, -boxCastSize.y / 2, 0), transform.position + boxCastOffset + new Vector3(boxCastSize.x / 2, -boxCastSize.y / 2, 0) - new Vector3(0, boxCastDistance, 0));
			Gizmos.DrawLine(transform.position + boxCastOffset - new Vector3(boxCastSize.x / 2, boxCastSize.y / 2, 0) - new Vector3(0, boxCastDistance, 0), transform.position + boxCastOffset + new Vector3(boxCastSize.x / 2, -boxCastSize.y / 2, 0) - new Vector3(0, boxCastDistance, 0));
		}
	}
}
