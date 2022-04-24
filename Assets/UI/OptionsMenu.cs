using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField] Animator optionsAC;
	bool isOptionsOn = false;

	[SerializeField] Slider bgmVolumeSlider;
	[SerializeField] Slider seVolumeSlider;
	[SerializeField] AudioMixer mainMixer;
	[SerializeField] float minVolume = -30;
	[SerializeField] float maxVolume = 0;

	private void Start()
	{
		bgmVolumeSlider.minValue = minVolume;
		bgmVolumeSlider.maxValue = maxVolume;
		seVolumeSlider.minValue = minVolume;
		seVolumeSlider.maxValue = maxVolume;
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

	public void SetBGMVolume(float volume)
	{
		mainMixer.SetFloat("bgmVolume", volume);
	}

	public void SetSEVolume(float volume)
	{
		mainMixer.SetFloat("seVolume", volume);
	}

	void ResetSliderValue()
	{
		float currentBgmVolume;
		mainMixer.GetFloat("bgmVolume", out currentBgmVolume);
		bgmVolumeSlider.value = currentBgmVolume;
		float currentSeVolume;
		mainMixer.GetFloat("seVolume", out currentSeVolume);
		seVolumeSlider.value = currentSeVolume;
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
}
