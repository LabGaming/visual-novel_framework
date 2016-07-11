using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class Option : System.Object {
	public string name;
	public string content;
	public string nextEvent;

	public Option(string name, string content, string nextEvent){
		this.name = name;
		this.content = content;
		this.nextEvent = nextEvent;
	}

	public void connectOptionWithEvent(StoryEvent nextEvent){
		this.nextEvent = nextEvent.name;
	}
}

[Serializable]
public class StoryEvent : System.Object {
	public string name;
	public string content;
	public string actorName;
	public MovieTexture background; 
	public List<Option> Options = new List<Option>();
}

public class StoryEvents : MonoBehaviour {
	public List<StoryEvent> Events = new List<StoryEvent>();

	public void LoadStoryEvents(EE_NodeGraph EventGraph) {
		Events.Clear();
		foreach (EE_NodeSimpleDialog node in EventGraph.nodes) {
			Events.Add(node.simpleDialog);
		}
	}

	public StoryEvent getEvent (string eventName) {
		return this.Events.Find(x => x.name == eventName);
	}
}
