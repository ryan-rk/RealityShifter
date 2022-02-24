using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityState : MonoBehaviour
{
	[SerializeField] bool isInitialReal = true;
	[SerializeField] float realAlpha = 1f;
	[SerializeField] float notRealAlpha = 0.4f;
	[SerializeField] GameObject realityIndicatorPrefab;
	Color particleRealColor = new Color(0.7f, 1f, 0.6f, 0.7f);
	Color particleNotRealColor = new Color(1f, 0.68f, 0.8f, 0.7f);
	ContactHighlight contactHighlight;

	public bool isReal { get; private set; } = true;
	bool isManaged = false;
	bool isContacted = false;

	ParticleSystem realityParticle;
	SpriteRenderer sr;
	int realLayerIndex = 7;
	int notRealLayerIndex = 8;
	public event Action OnStateUpdated;

	private void Start()
	{
		if (!isManaged)
		{
			SetManagedState(true);
		}
		sr = GetComponent<SpriteRenderer>();
		var contactHighlights = GetComponentsInChildren<ContactHighlight>(true);
		if (contactHighlights.GetLength(0) > 0)
		{
			contactHighlight = contactHighlights[0];
		}
		GameObject realityIndicator = Instantiate(realityIndicatorPrefab, transform.position, Quaternion.identity, transform);
		realityParticle = realityIndicator.GetComponent<ParticleSystem>();
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
		SetReality(isInitialReal);
		SetParticleColor(isReal);
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
			Color spriteColor = sr.color;
			spriteColor.a = isReal ? realAlpha : notRealAlpha;
			sr.color = spriteColor;
			gameObject.layer = isReal ? realLayerIndex : notRealLayerIndex;
			if (isReal)
			{
				realityParticle.Play();
			}
			else
			{
				realityParticle.Pause();
			}
			OnStateUpdated?.Invoke();
		}
		else
		{
			SetParticleColor(RealityManager.Instance.currentPlaneIsReal);
		}
	}

	void SetParticleColor(bool isReal)
	{
		var particleMainModule = realityParticle.main;
		particleMainModule.startColor = isReal ? particleRealColor : particleNotRealColor;
	}

	private void OnDisable()
	{
		SetManagedState(false);
	}

}
