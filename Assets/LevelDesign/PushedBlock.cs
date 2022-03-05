using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedBlock : MonoBehaviour
{
	bool isDestroying = false;
	Rigidbody2D rb;
	[SerializeField] Animator animator;
	[SerializeField] NotRealDetector notRealDetector;
	[SerializeField] ParticleSystem destroyParticle;
	[SerializeField] float destroyParticleDelay = 0.1f;

	private void OnEnable()
	{
		notRealDetector.OnTrappedShifting += SetDestroy;
	}

	private void OnDisable()
	{
		notRealDetector.OnTrappedShifting -= SetDestroy;
	}

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SetDestroy()
	{
		if (!isDestroying)
		{
			rb.bodyType = RigidbodyType2D.Kinematic;
			rb.velocity = Vector2.zero;
			animator.Play("Destroy");
			StartCoroutine(DelayDestroyParticle());
		}
	}

	IEnumerator DelayDestroyParticle()
	{
		yield return new WaitForSeconds(destroyParticleDelay);
		destroyParticle.Play();
	}
}
