using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

[Serializable]
public class EE_NodeSimpleDialog : EE_NodeBase {
	#region Variables
	public StoryEvent simpleDialog;
	#endregion

	#region Main Methods
	public override void InitNode ()
	{
		base.InitNode ();
		nodeType = NodeType.SimpleDialog;
		nodeRect = new Rect(10f, 10f, 150f, 65f);
		simpleDialog = new StoryEvent();
		simpleDialog.name = "Simple Dialog";
		simpleDialog.content = "Simple Dialog";
	}

	public override void UpdateNode (Event e, Rect viewRect)
	{
		base.UpdateNode (e, viewRect);
	}

	#if UNITY_EDITOR
    public override void DrawNodeProperties ()
	{
		GUILayout.Space(20);
		// Title and input Field 
		GUILayout.BeginVertical();
			simpleDialog.name = EditorGUILayout.DelayedTextField("Name:", simpleDialog.name);
			GUILayout.Space(20);
			simpleDialog.content = EditorGUILayout.DelayedTextField("Content:", simpleDialog.content);
		GUILayout.EndVertical();
		GUILayout.Space(20);
	}
	public override void UpdateNodeGUI (Event e, Rect viewRect)
	{
		base.UpdateNodeGUI (e, viewRect);
		// TODO2: Move most of this logic to EE_NodeBase so it will handle multiple inputs and outputs
		// TODO: Convert this into LinkedStoryEvent entry, for now we will have just one.
		if(GUI.Button(new Rect(
			nodeRect.x - nodeRect.width * .15f, 
			nodeRect.y + nodeRect.height * .5f -12f,
			nodeRect.width * .15f, 24f), "I")){
			if (parentGraph.nodeToConnect != null) {
				input.inputNode = parentGraph.nodeToConnect;
				input.inputOption = parentGraph.optionToConnect;
				input.isOccupied = input.inputNode != null;
				parentGraph.nodeToConnect.outputs[parentGraph.optionToConnect].isOccupied = input.isOccupied;
				parentGraph.nodeToConnect.outputs[parentGraph.optionToConnect].outputNode = this;
				// TODO: Update this so it works with multiple options, for now we consider we always have just one option
				((EE_NodeSimpleDialog) input.inputNode).simpleDialog.Options[parentGraph.optionToConnect].connectOptionWithEvent(
					this.simpleDialog
				);
				parentGraph.nodeToConnect = null;
				parentGraph.optionToConnect = -1;

			} else {
				EditorUtility.DisplayDialog(" Error Message", "Why are you clicking an input... ?", "Ehm...");
			}
		}

		renderOptions();

		nodeName = simpleDialog.name;
	}
	#endif
	#endregion

	#region Render Methods
	private void renderOptions(){
		nodeRect.height = 65f + 16.25f * (simpleDialog.Options.Count);
		for (int i = 0; i < simpleDialog.Options.Count; i++) {
			if(GUI.Button(new Rect(
				nodeRect.x + nodeRect.width * .75f, 
				nodeRect.y + nodeRect.height * .5f -12f * (1 - 2 * i),
				nodeRect.width * .5f, 24f), simpleDialog.Options[i].name)){
				parentGraph.nodeToConnect = this;
				parentGraph.optionToConnect = i;
				Debug.Log(parentGraph.optionToConnect);
			}
		} 
	}
	#endregion
}
