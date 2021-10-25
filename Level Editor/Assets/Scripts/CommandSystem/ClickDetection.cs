using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ClickDetection : MonoBehaviour
{
	public RectTransform UItransform;
	public int pixelHeight = 1080;
	public EditorManager manager;
	public Vector2 lowerBound = -Vector2.one;
	public Vector2 upperBound = Vector2.one;

	public GameObject prefab;
	PrefabFactory factory = new PrefabFactory();

    // Start is called before the first frame update
    void Start()
    {
		factory.prefab = prefab;
    }

    // Update is called once per frame
    void Update()
    {
		if (!EditorManager.GetPaused())	return;


		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (AABBTest(ConvertedMousePos())) {
				//manager.SetMoveHandle(factory.SpawnThing());
			}
		}
    }

	//relative to Canvas data
	public Vector2 ConvertedMousePos()
	{
		Vector2 pos = manager.editorCamera.ScreenToViewportPoint(Input.mousePosition);
		pos = pos * 2f - Vector2.one;
		pos.y *= pixelHeight * 0.5f;
		pos.x *= pixelHeight * 0.5f * manager.editorCamera.aspect;
		return pos;
	}

	bool AABBTest(Vector2 input) {
		return (
			lowerBound.x < input.x && input.x < upperBound.x ||
			lowerBound.y < input.y && input.y < upperBound.y
		);
	}
}
