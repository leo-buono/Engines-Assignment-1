using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Spawnable
{
	public GameObject prefab;
	public Block() {
}

    public GameObject SpawnThing() {
		return GameObject.Instantiate(prefab);
	}
}
