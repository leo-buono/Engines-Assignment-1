using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactory : Spawnable
{
	public GameObject prefab;
	public PrefabFactory() {}

    public GameObject SpawnThing() {
		return GameObject.Instantiate(prefab);
	}
}
