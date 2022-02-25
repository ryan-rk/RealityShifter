using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsTrigger : MonoBehaviour
{
	[SerializeField] GameObject tipsWindow;
	Animator tipsWindowAnimator;

	// Start is called before the first frame update
	void Start()
	{
		tipsWindow.gameObject.SetActive(false);
		tipsWindowAnimator = tipsWindow.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Player>())
		{
			if (tipsWindow.gameObject.activeSelf)
			{
				tipsWindowAnimator.Play("Expand");
			}
			else
			{
				tipsWindow.gameObject.SetActive(true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Player>())
		{
			tipsWindowAnimator.Play("Shrink");
		}
	}
}
