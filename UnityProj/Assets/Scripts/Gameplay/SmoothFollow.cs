using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = Vector3.back * 10f;
	private Vector3 diff = Vector3.zero;
	public float xFollowSpeed = 5f;
	public float yFollowSpeed = 10f;
	public float horizontalDirectionalOffset = 5f;
	public float horizontalOffsetSpeed = 2.5f;
	static public bool movingRight = false;
	static public bool advancedFocus = false;
	private float xOffsetTarget = 0f;
	private float x = 0f;
	private float y = 0f;

    void LateUpdate()
    {
		if (advancedFocus) {
			if (movingRight)
				xOffsetTarget = horizontalDirectionalOffset;
			else
				xOffsetTarget = -horizontalDirectionalOffset;
		}
		else xOffsetTarget = 0;

		offset.x = Mathf.Lerp(offset.x, xOffsetTarget, Time.deltaTime * horizontalOffsetSpeed);

		//high speed adjustments
		diff = target.position + offset;
		x = Mathf.Lerp(transform.position.x, diff.x, Time.deltaTime * xFollowSpeed);
		if (Mathf.Abs(x - diff.x) < 1f) {
			x = diff.x;
		}
		y = Mathf.Lerp(transform.position.y, diff.y, Time.deltaTime * yFollowSpeed);
		if (Mathf.Abs(y - diff.y) < 1f) {
			y = diff.y;
		}
		//percentage based follow
        transform.position = Vector3.forward * diff.z + Vector3.right * x + Vector3.up * y;

		//in case I add player rotation
		//transform.rotation = target.rotation;
    }
}
