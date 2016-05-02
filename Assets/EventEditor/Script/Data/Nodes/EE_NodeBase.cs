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
	public string nodeRect;
	public EE_NodeGraph parentGraph;
	#endregion

	#region Main Methods
	public virtual void InitNode() {}
	public virtual void UpdateNode(Event e, Rect viewRect){}
	#if UNITY_EDITOR
	public virtual void UpdateNodeGUI(Event e, Rect viewRect){
		ProcessEvents(e,viewRect);
	}
	#endif
	#endregion

	#region Utility Methods
	void ProcessEvents(Event e, Rect viewRect){}
	#endregion

}
