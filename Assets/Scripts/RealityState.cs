using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityState : MonoBehaviour
{
	[SerializeField] float realAlpha = 1f;
	[SerializeField] float notRealAlpha = 0.4f;
	[SerializeField] ParticleSystem realityParticle;
	[SerializeField] Color particleRealColor;
	[SerializeField] Color particleNotRealColor;
	public bool isReal { get; private set; } = true;
	bool isManaged = false;
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

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			SetReality(!isReal);
		}
	}

	public void SetReality(bool isReal)
	{
		this.isReal = isReal;
		Color spriteColor = sr.color;
		spriteColor.a = isReal ? realAlpha : notRealAlpha;
		sr.color = spriteColor;
		gameObject.layer = isReal ? realLayerIndex : notRealLayerIndex;
		SetParticleColor(isReal);
		OnStateUpdated?.Invoke();
	}

	void SetParticleColor(bool isReal)
	{
		var particleMainModule = realityParticle.main;
		particleMainModule.startColor = isReal ? particleRealColor : particleNotRealColor;
		Debug.Log("particle color changed");
	}

	private void OnDisable()
	{
		SetManagedState(false);
	}
}
