using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityState : MonoBehaviour
{
	[SerializeField] SpriteRenderer sr;
	// [SerializeField] SpriteRenderer realSR;
	// [SerializeField] SpriteRenderer notRealSR;
	[SerializeField] bool isInitialReal = true;
	[SerializeField] Material realMaterial;
	[SerializeField] Material notRealMaterial;
	// [SerializeField] float realAlpha = 1f;
	// [SerializeField] float notRealAlpha = 0.4f;
	// [SerializeField] GameObject realityIndicatorPrefab;
	// Color particleRealColor = new Color(0.7f, 1f, 0.6f, 0.7f);
	// Color particleNotRealColor = new Color(1f, 0.68f, 0.8f, 0.7f);
	ContactHighlight contactHighlight;

	[SerializeField] GameObject groundBody;

	public bool isReal { get; private set; } = true;
	bool isManaged = false;
	bool isContacted = false;

	// ParticleSystem realityParticle;
	int realLayerIndex = 7;
	int notRealLayerIndex = 8;
	public event Action<bool> OnStateUpdated;

	private void Start()
	{
		if (!isManaged)
		{
			SetManagedState(true);
		}
		var contactHighlights = GetComponentsInChildren<ContactHighlight>(true);
		if (contactHighlights.GetLength(0) > 0)
		{
			contactHighlight = contactHighlights[0];
		}
		// GameObject realityIndicator = Instantiate(realityIndicatorPrefab, transform.position, Quaternion.identity, transform);
		// realityParticle = realityIndicator.GetComponent<ParticleSystem>();
		InitializeReality();
		// SetReality(isInitialReal);
	}

	private void OnEnable()
	{
		if (!isManaged)
		{
			SetManagedState(true);
		}
	}

	void SetManagedState(bool isManaged)
	{
		if (RealityManager.Instance != null)
		{
			if (isManaged)
			{
				RealityManager.Instance.AddToManaged(this);
			}
			else
			{
				RealityManager.Instance.RemoveFromManaged(this);
			}
			this.isManaged = isManaged;
		}
	}

	void InitializeReality()
	{
		// SetSprite(isInitialReal);
		// SetParticleColor(isReal);
		SetReality(isInitialReal);
	}

	public void SetContacted(bool isContacted)
	{
		this.isContacted = isContacted;
		contactHighlight?.gameObject.SetActive(isContacted);
	}

	public void SetReality(bool isReal)
	{
		if (!isContacted)
		{
			this.isReal = isReal;
			// Color spriteColor = sr.color;
			// spriteColor.a = isReal ? realAlpha : notRealAlpha;
			// sr.color = spriteColor;
			gameObject.layer = isReal ? realLayerIndex : notRealLayerIndex;
			sr.material = isReal ? realMaterial : notRealMaterial;
			// if (isReal)
			// {
			// 	realityParticle.Play();
			// }
			// else
			// {
			// 	realityParticle.Pause();
			// }
			if (groundBody != null)
			{
				groundBody.SetActive(isReal);
			}
			OnStateUpdated?.Invoke(isReal);
		}
		// else
		// {
		// 	SetParticleColor(RealityManager.Instance.currentPlaneIsReal);
		// 	SetSprite(RealityManager.Instance.currentPlaneIsReal);
		// }
	}

	// void SetParticleColor(bool isReal)
	// {
	// 	var particleMainModule = realityParticle.main;
	// 	particleMainModule.startColor = isReal ? particleRealColor : particleNotRealColor;
	// }

	// void SetSprite(bool isReal)
	// {
	// 	if (realSR == null || notRealSR == null)
	// 	{
	// 		return;
	// 	}
	// 	sr.sprite = isReal ? realSR.sprite : notRealSR.sprite;
	// }

	private void OnDisable()
	{
		SetManagedState(false);
	}

}
