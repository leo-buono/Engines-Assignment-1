using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonHealth : MonoBehaviour
{
	public float health = 25;
    public static SingletonHealth instance {get; private set;}

	private void Awake()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}
}
