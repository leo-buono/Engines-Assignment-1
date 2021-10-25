using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThing : FunctionData
{
	public MoveThing(GameObject reference, Vector3 oldPosition, Vector3 newPosition)
	{
		this.reference = reference;
		this.oldPosition = oldPosition;
		this.newPosition = newPosition;
	}

	GameObject reference = null;
	Vector3 oldPosition;
	Vector3 newPosition;

	public void Execute()
	{
		if (reference != null)
			reference.transform.position = newPosition;
	}

	public void Undo()
	{
		if (reference != null)
			reference.transform.position = oldPosition;
	}

	public void Delete()
	{
		reference = null;
	}
}
