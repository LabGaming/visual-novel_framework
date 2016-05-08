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
	public EE_NodeWorkView () : base("Work View") {
		contextMenuOptions.Clear();
		contextMenuOptions.Add("Create Graph", new GenericMenu.MenuFunction2(this.CreateGrapOptionhCallback));
		contextMenuOptions.Add("Load Graph", new GenericMenu.MenuFunction2(this.LoadGraphOptionCallback));
	}
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
	#endregion

	#region Utility Methods
	void CreateGrapOptionhCallback (object obj){
		EE_PopUpWindow.InitNodePopUp();
	}

	void LoadGraphOptionCallback (object obj){
		EE_NodeUtils.LoadGraph();
	}
	#endregion
}
