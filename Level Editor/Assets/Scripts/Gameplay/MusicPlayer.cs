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
		ActionEventPlayer.instance.music += PlayAudio;
		ActionEventPlayer.instance.Music();
	}

	private void OnDisable()
	{
		ActionEventPlayer.instance.music -= PlayAudio;
	}

	private void PlayAudio()
	{
		_audiosource.Play();
	}
}
