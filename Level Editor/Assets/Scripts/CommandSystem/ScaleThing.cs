using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleThing : FunctionData
{
	public ScaleThing(GameObject reference, Vector3 oldScale, Vector3 newScale)
	{
		this.reference = reference;
		this.oldScale = oldScale;
		this.newScale = newScale;
	}

	GameObject reference = null;
	Vector3 oldScale;
	Vector3 newScale;

	public void Execute()
	{
		if (reference != null)
			reference.transform.localScale = newScale;
	}

	public void Undo()
	{
		if (reference != null)
			reference.transform.localScale = oldScale;
	}

	public void Delete()
	{
		reference = null;
	}
}
