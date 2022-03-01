using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public Sound[] sounds;
	[SerializeField] GameObject soundEffectContainer;
	[SerializeField] GameObject bgmContainer;
	[SerializeField] float fadeInSpeed = 1f;
	[SerializeField] float fadeOutSpeed = 1f;

	private void Awake()
	{
		// if (Instance == null)
		// {
		// 	Instance = this;
		// }
		// else
		// {
		// 	Destroy(gameObject);
		// 	return;
		// }
		// DontDestroyOnLoad(gameObject);
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}

		foreach (Sound sound in sounds)
		{
			if (sound.isSoundEffect)
			{
				sound.source = soundEffectContainer.AddComponent<AudioSource>();
			}
			else
			{
				sound.source = bgmContainer.AddComponent<AudioSource>();
			}
			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;
			sound.source.playOnAwake = false;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			Sound targetSound = Array.Find<Sound>(sounds, sound => sound.soundName == "StartMenu");
			Debug.Log(targetSound.source.isPlaying);
		}
	}

	public void PlaySound(string name, float startVolume)
	{
		Sound targetSound = Array.Find<Sound>(sounds, sound => sound.soundName == name);
		if (targetSound == null)
		{
			Debug.LogWarning("Sound not found");
			return;
		}
		if (!targetSound.isSoundEffect && targetSound.source.isPlaying)
		{
			Debug.Log("Sound is already playing: " + targetSound.soundName);
			return;
		}
		targetSound.source.volume = startVolume;
		targetSound.source.Play();
		if (startVolume != targetSound.volume)
		{
			StartCoroutine(FadeInProcess(targetSound.source, targetSound.volume));
		}
	}

	public void PlaySound(string name)
	{
		Sound targetSound = Array.Find<Sound>(sounds, sound => sound.soundName == name);
		if (targetSound == null)
		{
			Debug.LogWarning("Sound not found");
			return;
		}
		if (!targetSound.isSoundEffect && targetSound.source.isPlaying)
		{
			Debug.Log("Sound is already playing: " + targetSound.soundName);
			return;
		}
		targetSound.source.Play();
	}

	IEnumerator FadeInProcess(AudioSource source, float targetVolume)
	{
		while (source.volume < targetVolume)
		{
			yield return null;
			source.volume = Mathf.MoveTowards(source.volume, targetVolume, fadeInSpeed * Time.deltaTime);
		}
	}

	public void StopSound(string name, bool isFade)
	{
		Sound targetSound = Array.Find<Sound>(sounds, sound => sound.soundName == name);
		if (targetSound == null)
		{
			Debug.LogWarning("Sound not found");
			return;
		}
		if (isFade)
		{
			StartCoroutine(FadeOutProcess(targetSound.source));
		}
		else
		{
			targetSound.source.Stop();
		}
	}

	IEnumerator FadeOutProcess(AudioSource source)
	{
		while (source.volume > 0)
		{
			yield return null;
			source.volume = Mathf.MoveTowards(source.volume, 0, fadeOutSpeed * Time.deltaTime);
		}
		source.Stop();
	}
}
