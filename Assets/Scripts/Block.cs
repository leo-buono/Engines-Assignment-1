using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Spawnable
{
	public static GameObject prefab;
	public Block(Vector3 position) {
		this.position = position;
	}
	Vector3 position;

    public GameObject SpawnThing() {
		return GameObject.Instantiate(prefab, position, Quaternion.identity);
	}
}
