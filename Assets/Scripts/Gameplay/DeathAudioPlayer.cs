using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathAudioPlayer : MonoBehaviour
{
    private AudioSource _audiosource;

    private void Awake() {
        _audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
		ActionAudioPlayer.died += PlayAudio;
    }

    private void OnDisable() {
		ActionAudioPlayer.died -= PlayAudio;
    }

    private void PlayAudio(){
        _audiosource.Play();
    }
}
