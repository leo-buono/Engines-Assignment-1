using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWin : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject == Player.mainPlayer.gameObject) {
            SceneChanger.LoadWin();
		}
	}
}
