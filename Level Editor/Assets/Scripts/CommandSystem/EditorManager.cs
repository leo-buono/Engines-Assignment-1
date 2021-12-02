using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EditorManager : MonoBehaviour
{
	public Camera gameCamera;
	public Camera editorCamera;
	public Transform selectedOverlay;
	public Vector3 selectionBoost = Vector3.one * 0.15f;
	public float cameraDistance = 15f;
	public float speed = 5f;
	public float zoomSpeed = 5f;
	public Toggle orthoToggle;
	public Slider speedController;
	public Slider xScale;
	public Slider yScale;
	public Slider zScale;
	public InputField objName;
	public Image deletePrompt;
	public Image scalingPrompt;

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
		deletePrompt.gameObject.SetActive(false);
		selectedOverlay.gameObject.SetActive(false);
    }

	GameObject touch;
	GameObject lastTouch;
	Vector3 offset = Vector3.zero;
	Vector3 oldVec = Vector3.zero;
	bool dragging = false;
	bool scaling = false;
	bool typing = false;
	
    // Update is called once per frame
    void Update()
	{
		//dont anything while typing
		if (typing)		return;

		//toggle pause
		if (Input.GetKeyDown(KeyCode.P)) {
			TogglePause();
			selectedOverlay.gameObject.SetActive(paused && touch != null);
			return;
		}

		//dont do stuff if not paused
		if (!paused)	return;

		CameraMove();

		Vector3 pos = GetMousePos();

		if (Input.GetKeyDown(KeyCode.Delete)) {
			if (touch != null) {
				//don't delete unless prompt says so
				if (deletePrompt.gameObject.activeInHierarchy)
					CommandManager.instance.QueueFunction(new DeleteThing(touch));
			}
			if (scaling)
				scalingPrompt.color = Color.white;
			scaling = false;
			dragging = true;
			objName.text = "";
			dragging = false;
			touch = null;
			deletePrompt.gameObject.SetActive(false);
		}

		if (scaling) {
			touch.transform.localScale =
				Vector3.right * xScale.value +
				Vector3.up * yScale.value +
				Vector3.forward * zScale.value;

			if (Input.GetKeyDown(KeyCode.Mouse1)) {
				scalingPrompt.color = Color.white;
				scaling = false;
				CommandManager.instance.QueueFunction(new ScaleThing(touch, oldVec, touch.transform.localScale));
				//dragging = true;
				//objName.text = "";
				//dragging = false;
				//touch = null;
				//deletePrompt.gameObject.SetActive(false);
			}
		}
		else {
			//check touch
			if (dragging && touch != null)
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
					xScale.value = touch.transform.localScale.x;
					yScale.value = touch.transform.localScale.y;
					zScale.value = touch.transform.localScale.z;

					dragging = true;
					objName.text = touch.name;
					dragging = false;

					//don't show prompt if player or bomb
					deletePrompt.gameObject.SetActive(touch.layer != 6 && touch.layer != 9);
					selectedOverlay.gameObject.SetActive(true);

					if (touch == lastTouch) {
						//drag
						dragging = true;
						oldVec = touch.transform.position;
						offset = oldVec - pos;
					}
				}
				else {
					dragging = true;
					objName.text = "";
					dragging = false;
					touch = null;
					deletePrompt.gameObject.SetActive(false);
				}
			}
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			if (touch != null && dragging)
				CommandManager.instance.QueueFunction(new MoveThing(touch, oldVec, touch.transform.position));
			dragging = false;
		}

		//updated the selected
		if (touch != null) {
			selectedOverlay.position = touch.transform.position;
			selectedOverlay.localScale = touch.transform.lossyScale + selectionBoost;
			selectedOverlay.rotation = touch.transform.rotation;
		}
		else if (selectedOverlay.gameObject.activeInHierarchy) {
			selectedOverlay.gameObject.SetActive(false);
		}
    }

	void CameraMove()
	{
		//move camera
		cameraDistance -= Input.GetAxis("Zoom") * zoomSpeed * Time.deltaTime;
		if (cameraDistance < 0f)
			cameraDistance = 0f;


		speed = speedController.value;
		Vector3 wasd = editorCamera.transform.position;
		if (Input.GetButtonDown("Snap")) {
			//wasd = GameObject.Find("Player").transform.position;
			wasd = Player.mainPlayer.transform.position;
		}
		wasd.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		wasd.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
		if (orthoToggle.isOn) {
			editorCamera.orthographicSize = cameraDistance;
			wasd.z = -10f;
		}
		else {
			wasd.z = -cameraDistance;
		}
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

	public void SetName() {
		typing = false;
		if (lastTouch != null) {
			lastTouch.name = objName.text;
		}
	}
	public void StartTyping() {
		if (!dragging) {
			typing = true;
		}
	}

	public void StartScaling() {
		if (touch != null) {
			//dirty flag
			if (!scaling) {
				scalingPrompt.color = Color.grey + Color.green;
				scaling = true;
				oldVec = touch.transform.localScale;
			}
		}
	}

	public void ClearHistory() {
		CommandManager.instance.Clear();
	}

	public void UpdateCamera() {
		gameCamera.enabled = !paused;
		editorCamera.enabled = paused;
	}

	public void OrthoToggle() {
		editorCamera.orthographic = orthoToggle.isOn;
	}

	public Vector3 GetMousePos() {
		Vector3 pos = Input.mousePosition;
		pos.z = cameraDistance;
		return editorCamera.ScreenToWorldPoint(pos);
	}

	public void SetMoveHandle(GameObject obj) {
		//make sure to not scale no more
		if (scaling) {
			scalingPrompt.color = Color.white;
			scaling = false;
			CommandManager.instance.QueueFunction(new ScaleThing(touch, oldVec, touch.transform.localScale));
		}

		lastTouch = touch;
		touch = obj;
		xScale.value = touch.transform.localScale.x;
		yScale.value = touch.transform.localScale.y;
		zScale.value = touch.transform.localScale.z;

		dragging = true;
		objName.text = touch.name;

		deletePrompt.gameObject.SetActive(true);
		selectedOverlay.gameObject.SetActive(true);

		oldVec = touch.transform.position;
		offset = oldVec - GetMousePos();
	}
}
