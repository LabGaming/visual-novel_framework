using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EE_NodeGraph : ScriptableObject {
	#region Public Variables
	public string graphName = "New Graph";
	public List<EE_NodeBase> nodes;
	public EE_NodeBase selectedNode;
	public EE_NodeBase nodeToConnect;
	public int optionToConnect;
	#endregion

	#region Main Methods
	void OnEnable() {
		if (nodes == null) {
			nodes = new List<EE_NodeBase>();
		}
	}

	public void InitGraph(){
		if (nodes.Count > 0) {
			for (int i = 0; i < nodes.Count; i++) {
				nodes[i].InitNode();
			}
		}
	}

	#if UNITY_EDITOR
	public void UpdateGraphGUI(Event e, Rect viewRect) {
		if (nodes.Count > 0) {
			ProcessEvents(e, viewRect);
			foreach (var node in nodes) {
				node.UpdateNodeGUI(e, viewRect);
			}

			if (nodeToConnect != null) {
				nodeToConnect.DrawConnectionToMouse(e.mousePosition, optionToConnect);
			}
		}
		EditorUtility.SetDirty(this);
	}
	#endif
	#endregion

	#region Utility Methods
	void ProcessEvents(Event e, Rect viewRect) {
		if (viewRect.Contains(e.mousePosition)) {
			if(e.button == 0 & e.type == EventType.MouseDown) {
				CleanNodes();
				selectedNode = null;

				foreach (var node in nodes) {
					if (node.nodeRect.Contains(e.mousePosition)) {
						node.isSelected = true;
						selectedNode = node;
					}
				}

				if (selectedNode == null) {
					CleanNodes();
				}
			}
		}
	}

	void CleanNodes() {
		foreach (var node in nodes) {
			node.isSelected = false;
		}
	}
	#endregion
}
