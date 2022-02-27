using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
	public string soundName;
	public AudioClip clip;
	public bool isSoundEffect;
	public bool loop;

	public float volume = 1f;
	public float pitch = 0.2f;

	[HideInInspector]
	public AudioSource source;
}
