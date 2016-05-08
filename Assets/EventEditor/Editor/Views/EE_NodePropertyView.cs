﻿using UnityEngine;
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
	public override void UpdateView (Rect editorRect, Rect percentageRect, Event e, EE_NodeGraph currentGraph){
		base.UpdateView(editorRect, percentageRect, e, currentGraph);
	}

	public override void ProcessEvents (Event e){
		base.ProcessEvents(e);
	}
	#endregion

	#region Utility Methods
	protected override void CreateAreaContent (){}
	#endregion
}