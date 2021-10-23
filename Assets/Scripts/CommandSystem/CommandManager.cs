using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
	//undo system based on tutorial code
	static List<FunctionData> history = new List<FunctionData>();
	static Queue<FunctionData> queue = new Queue<FunctionData>();
	
	static void QueueFunction(FunctionData function) {
		if (historyPos < history.Count) {
			history.RemoveRange(historyPos, history.Count - historyPos);
		}
		queue.Enqueue(function);
	}

	static void Clear() {
		history.Clear();
		queue.Clear();
	}

	FunctionData tempFunc;
	static int historyPos = 0;

	public GameObject blah;

	void Start()
	{
		historyPos = history.Count;
		Block.prefab = blah;
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.G)) {
			QueueFunction(new SpawnThing(new Block(new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)))));
		}

        while (queue.Count > 0) {
			tempFunc = queue.Dequeue();
			tempFunc.Execute();
			history.Add(tempFunc);
			++historyPos;
			tempFunc = null;
		}

		if (history.Count > 0) {
			//allow undo/redo if count is above 0

			if (Input.GetKeyDown(KeyCode.Z)) {
				if (historyPos > 0) {
					history[--historyPos].Undo();
				}
			}
			else if (Input.GetKeyDown(KeyCode.R)) {
				if (historyPos < history.Count) {
					history[historyPos++].Execute();
				}
			}
		}
    }
}
