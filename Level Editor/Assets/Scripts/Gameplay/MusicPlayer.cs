using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
	private AudioSource _audiosource;

	private void Awake()
	{
		_audiosource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		ActionAudioPlayer.instance.music += PlayAudio;
		ActionAudioPlayer.instance.Music();
	}

	private void OnDisable()
	{
		ActionAudioPlayer.instance.music -= PlayAudio;
	}

	private void PlayAudio()
	{
		_audiosource.Play();
	}
}
