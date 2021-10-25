using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ClickDetection : MonoBehaviour
{
	public RectTransform UItransform;
	public int pixelHeight = 1080;
	float camDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Vector3 pos = Input.mousePosition;
			pos.z = camDistance;
			Collider2D touch = CollisionCheck(Camera.main.ScreenToWorldPoint(pos));
			if (touch != null) {
				Debug.Log(touch.name);
			}
		}
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

	Collider2D CollisionCheck(Vector2 input)
	{
		return Physics2D.OverlapPoint(input);
	}
}
