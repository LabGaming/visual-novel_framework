using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EE_NodeWorkView : EE_ViewBase {
	#region Constructor
	public EE_NodeWorkView () : base("Work View") {}
	#endregion

	#region Private Variables
	Vector2 mousePositionForNewNode;
	#endregion

	#region Main Methods
	public override void UpdateView (Rect editorRect, Rect percentageRect, Event e, EE_NodeGraph currentGraph){
		base.UpdateView(editorRect, percentageRect, e, currentGraph);
		if(currentGraph != null) {
			viewTitle = currentGraph.graphName;
		} else {
			viewTitle = "Please Create a New Graph";
		}
	}

	protected override void LoadContextMenuOptions(Event e) {
		menu.AddItem(new GUIContent("Create Graph"), false, this.CreateGraphOptionCallback, e);
		menu.AddItem(new GUIContent("Load Graph"), false, this.LoadGraphOptionCallback, e);
		if(currentGraph != null){
			menu.AddSeparator("");
			menu.AddItem(new GUIContent("Add Event Node"), false, this.AddEventNodeOptionCallback, e);
			foreach (var node in currentGraph.nodes) {
				if(node.nodeRect.Contains(e.mousePosition)) {
					menu.AddSeparator("");
					menu.AddItem(
						new GUIContent("Add Event Option To " + node.nodeName), 
						false, 
						this.AddEventOptionToOptionCallback, 
						(object) node
					);
					menu.AddItem(
						new GUIContent("Delete Event Node " + node.nodeName), 
						false, 
						this.DeleteEventNodeOptionCallback, 
						(object) node
					);
					break;
				}
			}
		}
	}
	protected override void CreateAreaContent (Event e, Rect viewRect)
	{
		base.CreateAreaContent (e, viewRect);
		if(currentGraph != null){
			currentGraph.UpdateGraphGUI (e, viewRect);
		}
	}
	#endregion

	#region Utility Methods
	void CreateGraphOptionCallback (object e){
		EE_PopUpWindow.InitNodePopUp(PopUpType.CreateNode);
	}

	void LoadGraphOptionCallback (object e){
		EE_NodeUtils.LoadGraph();
	}
	void AddEventNodeOptionCallback (object e){
		EE_NodeUtils.CreateNode(
			NodeType.SimpleDialog,
			((Event) e).mousePosition
		);
	}
	void AddEventOptionToOptionCallback (object e) {
		EE_NodeSimpleDialog nodeToAddOption = (EE_NodeSimpleDialog) e;
		EE_PopUpWindow.InitNodePopUp(PopUpType.AddOption, nodeToAddOption);
	}
	void DeleteEventNodeOptionCallback (object e) {
		EE_NodeBase nodeToDelete = (EE_NodeBase) e;
		Debug.Log("Deleting Event Node " + nodeToDelete.nodeName);
		EE_NodeUtils.DeleteNode(nodeToDelete);
	}
	#endregion
}
