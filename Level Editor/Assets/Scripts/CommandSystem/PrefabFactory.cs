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

//example fast enemy factory, since prefab covered all our needs
/*
public class FastEnemyFactory : Spawnable
{
	public Enemy prefab;
	public EnemyFactory(float speed) {
		this.speed = speed;
	}

	float speed;
    public GameObject SpawnThing() {
		return GameObject.Instantiate(prefab);
	}
}
*/