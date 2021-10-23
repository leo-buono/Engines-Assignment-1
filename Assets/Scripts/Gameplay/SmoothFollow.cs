using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = Vector3.back * 10f;
	public float followSpeed = 5f;
	public float horizontalDirectionalOffset = 5f;
	public float horizontalOffsetSpeed = 2.5f;
	static public bool movingRight = false;
	static public bool advancedFocus = false;
	private float xOffsetTarget = 0f;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
		if (advancedFocus) {
			if (movingRight)
				xOffsetTarget = horizontalDirectionalOffset;
			else
				xOffsetTarget = -horizontalDirectionalOffset;
		}
		else xOffsetTarget = 0;

		offset.x = Mathf.Lerp(offset.x, xOffsetTarget, Time.deltaTime * horizontalOffsetSpeed);

		//percentage based follow
        transform.position = Vector3.Lerp(
			transform.position, target.position + offset, Time.deltaTime * followSpeed
			);

		//in case I add player rotation
		//transform.rotation = target.rotation;
    }
}
