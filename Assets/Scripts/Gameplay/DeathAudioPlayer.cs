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
		ActionAudioPlayer.instance.died += PlayAudio;
		ActionAudioPlayer.instance.damaged += PlayAudio;
    }

    private void OnDisable() {
		ActionAudioPlayer.instance.died -= PlayAudio;
		ActionAudioPlayer.instance.damaged -= PlayAudio;
    }

    private void PlayAudio(){
        _audiosource.Play();
    }
}
