using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWin : MonoBehaviour
{
	public GameObject player;

	float timer = 0f;

	void Update() {
		if (timer > 0f) {
			timer -=Time.deltaTime;
			if (timer <= 0f) {
            	SceneChanger.LoadWin();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject == player) {
			timer = 1f;
			collision.rigidBody.isKinematic = true;
		}
	}
}
