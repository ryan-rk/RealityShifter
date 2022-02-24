using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager Instance;

	public CinemachineVirtualCamera mainVcam;
	public ScreenShaker mainVcamScreenShaker;
	float originalLensSize;
	[SerializeField] float zoomShakeSize = 8f;
	[SerializeField] float zoomShakeSpeed = 0.1f;

	private void Awake()
	{
		Instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		originalLensSize = mainVcam.m_Lens.OrthographicSize;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ZoomShake()
	{
		mainVcam.m_Lens.OrthographicSize = zoomShakeSize;
		StopAllCoroutines();
		StartCoroutine(ZoomShakeProcess());
	}

	IEnumerator ZoomShakeProcess()
	{
		while (mainVcam.m_Lens.OrthographicSize != originalLensSize)
		{
			yield return null;
			float newSize = Mathf.MoveTowards(mainVcam.m_Lens.OrthographicSize, originalLensSize, zoomShakeSpeed * Time.deltaTime);
			mainVcam.m_Lens.OrthographicSize = newSize;
		}
	}
}
