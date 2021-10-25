using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EditorManager : MonoBehaviour
{
	public Camera gameCamera;
	public Camera editorCamera;
	public float cameraDistance = 15f;
	public float speed = 5f;
	public float zoomSpeed = 5f;
	public Toggle orthoToggle;
	public Slider speedController;	

	public InputField objName;

	static bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
		UpdateCamera();
		OrthoToggle();
		Vector3 pos = editorCamera.transform.position;
		pos.z = -cameraDistance;
		editorCamera.transform.position = pos;
		speedController.value = speed;



		blockMaker.prefab = block;
    }

	public GameObject block;
	PrefabFactory blockMaker = new PrefabFactory();

	GameObject touch;
	GameObject lastTouch;
	Vector3 offset = Vector3.zero;
	bool dragging = false;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P)) {
			TogglePause();
			return;
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			CommandManager.instance.QueueFunction(new SpawnThing(blockMaker, new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f))));
		}

		//dont do stuff if not paused
		if (!paused)	return;

		//dont allow movement while typing
		if (!objName.isFocused) 
			CameraMove();

		Vector3 pos = Input.mousePosition;
		pos.z = cameraDistance;
		pos = editorCamera.ScreenToWorldPoint(pos);

		//check touch
		if (dragging && touch! != null)
		{
			touch.transform.position = pos + offset;
		}
		else if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			lastTouch = touch;
			Collider2D temp = Physics2D.OverlapPoint(pos);
			if (temp != null)
			{
				touch = temp.gameObject;
				objName.text = touch.name;

				if (touch == lastTouch) {
					//drag
					dragging = true;
					offset = touch.transform.position - pos;
				}
			}
			else {
				objName.text = "";
				touch = null;
			}
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			dragging = false;
		}
    }

	void CameraMove()
	{
		//move camera
		cameraDistance -= Input.GetAxis("Zoom") * zoomSpeed * Time.deltaTime;
		if (cameraDistance < 0f)
			cameraDistance = 0f;

		if (orthoToggle.isOn)
		{
			editorCamera.orthographicSize = cameraDistance;
		}

		speed = speedController.value;
		Vector3 wasd = editorCamera.transform.position;
		wasd.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		wasd.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
		wasd.z = -cameraDistance;
		editorCamera.transform.position = wasd;
	}



	//less used things
	void FixedUpdate()
	{
		if (!paused)
			Physics2D.Simulate(Time.fixedDeltaTime);
	}

	public static bool GetPaused() {
		return paused;
	}

	public void TogglePause() {
		paused = !paused;
		UpdateCamera();
	}

	public void UpdateCamera() {
		gameCamera.enabled = !paused;
		editorCamera.enabled = paused;
	}

	public void OrthoToggle() {
		editorCamera.orthographic = orthoToggle.isOn;
	}

	public void SetMoveHandle(GameObject obj) {
		touch = obj;
		dragging = true;

		Vector3 pos = Input.mousePosition;
		pos.z = cameraDistance;
		offset = touch.transform.position - editorCamera.ScreenToWorldPoint(pos);
	}
}
