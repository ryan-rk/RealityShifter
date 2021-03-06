using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedBlock : MonoBehaviour
{
	bool isDestroying = false;
	Rigidbody2D rb;
	Collider2D col;
	[SerializeField] float dynamicChangeDelay = 0.5f;
	[SerializeField] Animator animator;
	[SerializeField] NotRealDetector notRealDetector;
	[SerializeField] HazardDetector hazardDetector;
	[SerializeField] ParticleSystem destroyParticle;
	[SerializeField] float destroyParticleDelay = 0.1f;

	private void OnEnable()
	{
		notRealDetector.OnTrappedShifting += SetDestroy;
		hazardDetector.OnHazardDetected += SetDestroy;
	}

	private void OnDisable()
	{
		notRealDetector.OnTrappedShifting -= SetDestroy;
		hazardDetector.OnHazardDetected -= SetDestroy;
	}

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		rb.bodyType = RigidbodyType2D.Kinematic;
		StartCoroutine(DelaySetDynamic());
	}

	IEnumerator DelaySetDynamic()
	{
		yield return new WaitForSeconds(dynamicChangeDelay);
		rb.bodyType = RigidbodyType2D.Dynamic;
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
			col.enabled = false;
			notRealDetector.gameObject.SetActive(false);
			animator.Play("Destroy");
			StartCoroutine(DelayDestroyParticle());
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.PlaySound("RockSmashed");
			}
		}
	}

	IEnumerator DelayDestroyParticle()
	{
		yield return new WaitForSeconds(destroyParticleDelay);
		destroyParticle.Play();
	}
}
