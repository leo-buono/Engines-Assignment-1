using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWin : MonoBehaviour
{
	GameObject player;

	void Start() {
		player = GameObject.Find("Player");
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject == player) {
            SceneChanger.LoadWin();
		}
	}
}
