using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteThing : FunctionData
{
	public DeleteThing(GameObject reference)
	{
		this.reference = reference;
	}

	public GameObject reference = null;

	public void Execute()
	{
		if (reference != null)
			reference.SetActive(false);
	}

	public void Undo()
	{
		if (reference != null)
			reference.SetActive(true);
	}

	public void Delete()
	{
		if (reference != null)
			if (!reference.activeInHierarchy)
				GameObject.Destroy(reference);
	}
}
