using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWin : MonoBehaviour
{
	public GameObject player;
    public SceneChanger manager;

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject == player)
            manager.LoadWin();
	}
}
