using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Option : System.Object {
	public string name;
	public string content;
	public string nextEvent;
}

[System.Serializable]
public class StoryEvent : System.Object {
	public string name;
	public string content;
	public List<Option> Options = new List<Option>();
}

public class StoryEvents : MonoBehaviour {
	public List<StoryEvent> Events = new List<StoryEvent>();

	public StoryEvent getEvent (string eventName) {
		return this.Events.Find(x => x.name == eventName);
	}
}
