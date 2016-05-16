using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

[Serializable]
public class EE_NodeBase : ScriptableObject {
	#region Public Variables
	public string nodeName;
	public Rect nodeRect;
	public EE_NodeGraph parentGraph;
	public NodeType nodeType;
	public bool isSelected = false;
	public EE_NodeInput input;
	public EE_NodeOutput output;
	#endregion

	#region Constructor
	public EE_NodeBase() {
		input = new EE_NodeInput();
		output = new EE_NodeOutput();
	}
	#endregion

	#region SubClasses
	[Serializable]
	public class EE_NodeInput {
		public bool isOccupied = false;
		public EE_NodeBase inputNode;
	}

	[Serializable]
	public class EE_NodeOutput {
		public bool isOccupied = false;
		public EE_NodeBase outputNode;
	}
	#endregion

	#region Main Methods
	public virtual void InitNode() {}
	public virtual void UpdateNode(Event e, Rect viewRect){}
	#if UNITY_EDITOR
    public virtual void DrawNodeProperties(){}
	public virtual void UpdateNodeGUI(Event e, Rect viewRect){
		ProcessEvents(e,viewRect);
		GUI.Box(nodeRect,nodeName);
		DrawConnectionToInputNode();

		EditorUtility.SetDirty(this);
	}
	#endif
	#endregion

	#region Utility Methods
	public void DrawConnectionToMouse(Vector2 mousePos) {
		Vector2 startPos = new Vector2(nodeRect.x + nodeRect.width*1.15f, nodeRect.y + nodeRect.height / 2);
		DrawConnection(startPos, mousePos);	
    }

	public void DrawConnectionToInputNode() {
		if (output.isOccupied) {
			Rect outNodeRect = output.outputNode.nodeRect;
			Vector2 startPos = new Vector2(nodeRect.x + nodeRect.width*1.15f, nodeRect.y + nodeRect.height / 2);
			Vector2 endPos = new Vector2(outNodeRect.x - outNodeRect.width*0.15f, outNodeRect.y + outNodeRect.height / 2);
			DrawConnection(startPos, endPos);	
		}
    }

    void DrawConnection(Vector2 startPos, Vector2 endPos) {
		Vector2 startTan = startPos + Vector2.right * 50;
		Vector2 endTan = endPos + Vector2.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) 
			Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
		Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

	void ProcessEvents(Event e, Rect viewRect){
		if( isSelected &
			viewRect.Contains(e.mousePosition) & 
			e.type == EventType.MouseDrag){
			nodeRect.x += e.delta.x;
			nodeRect.y += e.delta.y;
		}
	}
	#endregion

}
