using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetAttacher : MonoBehaviour
{
	List<GameObject> overlapObjects = new List<GameObject>();


	public void MoveOverlapObjects(Vector2 offset)
	{
		foreach (GameObject overlapObject in overlapObjects)
		{
			overlapObject.transform.position = (Vector2)overlapObject.transform.position + offset;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Player>())
		{
			overlapObjects.Add(other.gameObject);
			// other.transform.SetParent(transform);
		}
		// if (other.GetComponent<PushedBlock>())
		// {
		// 	overlapObjects.Add(other.gameObject);
		// 	// other.transform.SetParent(transform);
		// }
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Player>())
		{
			overlapObjects.Remove(other.gameObject);
			// other.transform.SetParent(null);
		}
		// if (other.GetComponent<PushedBlock>())
		// {
		// 	overlapObjects.Remove(other.gameObject);
		// 	// other.transform.SetParent(null);
		// }
	}

}
