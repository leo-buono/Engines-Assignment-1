using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
	//undo system based on tutorial code
	List<FunctionData> history = new List<FunctionData>();
	Queue<FunctionData> queue = new Queue<FunctionData>();
	
	public static CommandManager instance { get; private set; }

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this);
		}
		else {
			Destroy(gameObject);
		}
	}

	public void QueueFunction(FunctionData function) {
		if (historyPos < history.Count) {
			int index;
			while (historyPos < history.Count) {
				index = history.Count - 1;
				history[index].Delete();
				history.RemoveAt(index);
			}
		}
		queue.Enqueue(function);
	}

	public void Clear() {
		historyPos = 0;
		
		int index;
		while (historyPos < history.Count)
		{
			index = history.Count - 1;
			history[index].Delete();
			history.RemoveAt(index);
		}
		queue.Clear();
	}

	FunctionData tempFunc;
	static int historyPos = 0;

	void Start()
	{
		historyPos = history.Count;
	}

    void Update()
    {
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
