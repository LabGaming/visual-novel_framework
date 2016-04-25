using UnityEngine;
using System.Collections;

public class OptionDisplayer : MonoBehaviour {
	public Option option;
	public StoryEventsDisplayer displayer;

	public void displayNextEvent() {
		displayer.displayEvent(option.nextEvent);
	}
}