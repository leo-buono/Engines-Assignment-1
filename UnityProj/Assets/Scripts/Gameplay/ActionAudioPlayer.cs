using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionAudioPlayer : MonoBehaviour
{
	public static ActionAudioPlayer instance { get; private set; }

    public event Action died;
    public event Action damaged;
    public event Action music;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

    public void Die() {
        died?.Invoke();
    }

	public void Damaged() {
		damaged?.Invoke();
	}

	public void Music() {
		music?.Invoke();
	}
}
