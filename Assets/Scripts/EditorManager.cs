using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
	static bool paused = true;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P)) {
			paused = !paused;

		}

    }

	void FixedUpdate()
	{
		if (!paused)
			Physics2D.Simulate(Time.fixedDeltaTime);
	}

	public static bool GetPaused() {
		return paused;
	}
}
