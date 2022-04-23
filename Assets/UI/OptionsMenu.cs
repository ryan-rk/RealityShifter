using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField] Animator optionsAC;
	bool isOptionsOn = false;

	[SerializeField] Slider volumeSlider;
	[SerializeField] AudioMixer mainMixer;
	[SerializeField] float minVolume = -30;
	[SerializeField] float maxVolume = 0;

	private void Start()
	{
		volumeSlider.minValue = minVolume;
		volumeSlider.maxValue = maxVolume;
		ResetSliderValue();
	}

	public void ToggleOptions()
	{
		optionsAC.Play(isOptionsOn ? "SlideOut" : "SlideIn");
		isOptionsOn = !isOptionsOn;
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySound("Click");
		}
	}

	public void SetVolume(float volume)
	{
		mainMixer.SetFloat("masterVolume", volume);
	}

	void ResetSliderValue()
	{
		float currentMasterVolume;
		mainMixer.GetFloat("masterVolume", out currentMasterVolume);
		volumeSlider.value = currentMasterVolume;
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
}
