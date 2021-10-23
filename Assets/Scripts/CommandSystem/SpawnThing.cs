using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThing : FunctionData
{
	public SpawnThing(Spawnable spawnable) {
		type = spawnable;
	}

	Spawnable type;

	GameObject reference = null;

    public void Execute()
    {
        reference = type.SpawnThing();
    }

    public void Undo()
    {
        if (reference != null)
			GameObject.Destroy(reference);
    }
}
