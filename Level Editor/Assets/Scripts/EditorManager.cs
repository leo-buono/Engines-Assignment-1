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
	public Slider speedController;	



	static bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
		UpdateCamera();
		Vector3 pos = editorCamera.transform.position;
		pos.z = -cameraDistance;
		editorCamera.transform.position = pos;
		speedController.value = speed;



		blockMaker.prefab = block;
    }

	public GameObject block;
	PrefabFactory blockMaker = new PrefabFactory();

	GameObject touch;

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

		//move camera
		cameraDistance -= Input.GetAxis("Zoom") * zoomSpeed * Time.deltaTime;
		if (cameraDistance < 0f)
			cameraDistance = 0f;

		speed = speedController.value;
		Vector3 wasd = editorCamera.transform.position;
		wasd.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		wasd.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
		wasd.z = -cameraDistance;
		editorCamera.transform.position = wasd;


		//check touch
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Vector3 pos = Input.mousePosition;
			pos.z = cameraDistance;
			touch = Physics2D.OverlapPoint(editorCamera.ScreenToWorldPoint(pos)).gameObject;
			if (touch != null)
			{
				Debug.Log(touch.name);
			}
		}
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
}
