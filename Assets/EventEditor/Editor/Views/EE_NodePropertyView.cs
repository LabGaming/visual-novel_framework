using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EE_NodePropertyView : EE_ViewBase {
	#region Constructor
	public EE_NodePropertyView () : base("Property View") {}
	#endregion

	#region Main Methods
	protected override void CreateAreaContent (Event e, Rect viewRect){
		if (currentGraph != null && currentGraph.selectedNode != null) {
			currentGraph.selectedNode.DrawNodeProperties();
		}
	}
	#endregion
}
