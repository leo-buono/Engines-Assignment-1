using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SliderSelect : MonoBehaviour, ISelectHandler
{
    public EditorManager manager;
	public void OnSelect(BaseEventData data) {
		manager.StartScaling();
	}
}
