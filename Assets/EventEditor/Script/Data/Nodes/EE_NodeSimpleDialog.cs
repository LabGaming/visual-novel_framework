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
		simpleDialog.Options.Add(new Option("DefaultOption", "This is a Default Option", ""));
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
			nodeRect.x - nodeRect.width* 0.15f, 
			nodeRect.y + nodeRect.height*0.5f -12f,
			nodeRect.width* 0.15f, 24f), "I")){
			if (parentGraph.nodeToConnect != null) {
				input.inputNode = parentGraph.nodeToConnect;
				input.isOccupied = input.inputNode != null;
				parentGraph.nodeToConnect.output.isOccupied = input.isOccupied;
				parentGraph.nodeToConnect.output.outputNode = this;
				// TODO: Update this so it works with multiple options, for now we consider we always have just one option
				((EE_NodeSimpleDialog) input.inputNode).simpleDialog.Options[0].connectOptionWithEvent(
					this.simpleDialog
				);
				parentGraph.nodeToConnect = null;

			} else {
				EditorUtility.DisplayDialog(" Error Message", "Why are you clicking an input... ?", "Ehm...");
			}
		}

		// TODO: Convert this into renderOptions, one button for each option
		if(GUI.Button(new Rect(
			nodeRect.x + nodeRect.width, 
			nodeRect.y + nodeRect.height*0.5f -12f,
			nodeRect.width* 0.15f, 24f), "O")){
			parentGraph.nodeToConnect = this;
		} 

		nodeName = simpleDialog.name;
	}
	#endif
	#endregion
}
