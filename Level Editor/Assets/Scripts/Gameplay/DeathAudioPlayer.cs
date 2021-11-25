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
		ActionEventPlayer.instance.died += PlayAudio;
		ActionEventPlayer.instance.damaged += PlayAudio;
    }

    private void OnDisable() {
		ActionEventPlayer.instance.died -= PlayAudio;
		ActionEventPlayer.instance.damaged -= PlayAudio;
    }

    private void PlayAudio(){
        _audiosource.Play();
    }
}
