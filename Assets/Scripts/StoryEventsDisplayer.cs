using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class StoryEventsDisplayer : MonoBehaviour {
	
	public StoryEvents events;
	public Text eventDisplayer;
	public GameObject optionsDisplayer;
	public string initialEvent;
	public GameObject prefab;

	private StoryEvent currentEvent;

	void Start () {
		this.displayEvent(initialEvent);
	}

	void displayEvent (string eventName) {
		currentEvent = events.getEvent(eventName);
		eventDisplayer.text = currentEvent.content;
		displayOptionsForCurrentEvent();
	}

	void displayOptionsForCurrentEvent () {
		foreach (var item in currentEvent.Options) {
			GameObject g_go = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			g_go.GetComponentInChildren<OptionDisplayer>().option = item;
			g_go.GetComponentInChildren<Text>().text = item.content;
			g_go.transform.SetParent(optionsDisplayer.transform);
		}
	}
}
