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

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	//relative to Canvas data
	Vector2 ConvertedMousePos()
	{
		Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		pos = pos * 2f - Vector2.one;
		pos.y *= pixelHeight * 0.5f;
		pos.x *= pixelHeight * 0.5f * Camera.main.aspect;
		return pos;
	}
}
