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
		displayEvent(initialEvent);
	}

	#region Display Methods
	public void displayEvent (string eventName) {
		currentEvent = events.getEvent(eventName);
		Debug.Log(eventDisplayer.text);
		Debug.Log(currentEvent.content);
		eventDisplayer.text = currentEvent.content;
//		displayOptionsForCurrentEvent();
	}

	private void displayOptionsForCurrentEvent () {
		clearOptions();
		foreach (var item in currentEvent.Options) {
			GameObject g_go = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

			g_go.GetComponentInChildren<OptionDisplayer>().option = item;
			g_go.GetComponentInChildren<OptionDisplayer>().displayer = this;
			g_go.GetComponentInChildren<Button>().onClick.AddListener(
				g_go.GetComponentInChildren<OptionDisplayer>().displayNextEvent
			);
			g_go.GetComponentInChildren<Text>().text = item.content;
			g_go.transform.SetParent(optionsDisplayer.transform);
		}
	}

	private void clearOptions () {
		foreach (Transform child in optionsDisplayer.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
	#endregion
}
