using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThing : FunctionData
{
	public SpawnThing(Spawnable factory, Vector3 position) {
		this.factory = factory;
		this.position = position;
	}

	Spawnable factory;
	Vector3 position;

	GameObject reference = null;

    public void Execute()
    {
        reference = factory.SpawnThing();
		reference.transform.position = position;
    }

    public void Undo()
    {
        if (reference != null)
			GameObject.Destroy(reference);
    }
}
