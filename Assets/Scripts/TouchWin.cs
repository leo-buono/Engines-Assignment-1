using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWin : MonoBehaviour
{
	public GameObject player;
	public Transform newLocation;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == player)
			player.transform.position = newLocation.position;
	}
}
