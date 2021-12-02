using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ClickDetection : MonoBehaviour
{
	//public RectTransform UItransform;
	//public int pixelHeight = 1080;
	public EditorManager manager;
	private Vector2 lowerBound;
	private Vector2 upperBound;

	public GameObject prefab;
	PrefabFactory factory = new PrefabFactory();

    // Start is called before the first frame update
    void Start()
    {
		factory.prefab = prefab;
		//lowerBound += (Vector2)UItransform.localPosition;
		//upperBound += (Vector2)UItransform.localPosition;

		RectTransform trans = GetComponent<RectTransform>();
		Vector2 scale = manager.editorCamera.pixelRect.size / trans.GetComponentInParent<CanvasScaler>().referenceResolution;
		lowerBound = manager.editorCamera.pixelRect.size * trans.anchorMin
			+ (trans.anchoredPosition - (Vector2.one - trans.pivot) * trans.rect.size) * scale;
		upperBound = manager.editorCamera.pixelRect.size * trans.anchorMax
			+ (trans.anchoredPosition + trans.pivot * trans.rect.size) * scale;
    }

    // Update is called once per frame
    void Update()
    {
		if (!EditorManager.GetPaused())	return;


		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			//if (AABBTest(ConvertedMousePos())) {
			if (AABBTest(Input.mousePosition)) {
				SpawnThing spawn = new SpawnThing(factory, manager.GetMousePos());
				spawn.Execute();
				manager.SetMoveHandle(spawn.reference);
				CommandManager.instance.QueueFunction(spawn);
			}
		}
    }

	//relative to Canvas data
	/*
	public Vector2 ConvertedMousePos()
	{
		Vector2 pos = manager.editorCamera.ScreenToViewportPoint(Input.mousePosition);
		pos = pos * 2f - Vector2.one;
		pos.y *= pixelHeight * 0.5f;
		pos.x *= pixelHeight * 0.5f * manager.editorCamera.aspect;
		return pos;
	}
	*/

	bool AABBTest(Vector2 input) {
		return (
			lowerBound.x < input.x && input.x < upperBound.x &&
			lowerBound.y < input.y && input.y < upperBound.y
		);
	}
}
