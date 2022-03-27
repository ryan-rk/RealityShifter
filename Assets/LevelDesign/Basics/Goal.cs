using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	[SerializeField] ParticleSystem goalParticle;
	// RealityState realityState;
	Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		// realityState = GetComponent<RealityState>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		// if (realityState.isReal)
		// {
		// 	animator.speed = 1f;
		// 	if (!goalParticle.isPlaying)
		// 	{
		// 		goalParticle.Play();
		// 	}
		// }
		// else
		// {
		// 	animator.speed = 0f;
		// 	if (goalParticle.isPlaying)
		// 	{
		// 		goalParticle.Pause();
		// 	}
		// }
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<Player>(out Player player))
		{
			player.SetWin();
			GameManager.Instance.WinLevel();
		}
	}
}
