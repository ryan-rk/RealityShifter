using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShaker : MonoBehaviour
{
	CinemachineVirtualCamera CMVCam;
	CinemachineBasicMultiChannelPerlin CMNoise;

	// Start is called before the first frame update
	void Start()
	{
		CMVCam = GetComponent<CinemachineVirtualCamera>();
		CMNoise = CMVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ScreenShake(float intensity, float duration)
	{
		StopAllCoroutines();
		if (CMNoise != null)
		{
			CMNoise.m_AmplitudeGain = intensity;
		}
		StartCoroutine(ScreenShakeFalloff(duration));
	}

	IEnumerator ScreenShakeFalloff(float duration)
	{
		yield return new WaitForSeconds(duration);
		if (CMNoise != null)
		{
			CMNoise.m_AmplitudeGain = 0f;
		}
	}
}
