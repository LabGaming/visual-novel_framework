﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EE_ViewBase {
	#region Public Variables
	public string viewTitle;
	public Rect viewRect;
	#endregion

	#region Protected Variables
	protected GUISkin viewSkin;
	protected GenericMenu menu; 
	protected static Dictionary<String,GenericMenu.MenuFunction2> contextMenuOptions = new Dictionary<String,GenericMenu.MenuFunction2>();
	protected EE_NodeGraph currentGraph;
	#endregion

	#region Constructor
	public EE_ViewBase(string title) {
		viewTitle = title;
	}
	#endregion

	#region Main Methods
	public virtual void UpdateView(Rect editorRect, Rect percentageRect, Event e, EE_NodeGraph currentGraph) {
		this.currentGraph = currentGraph;

		// Updates view rectangle
		viewRect = new Rect(
			editorRect.x * percentageRect.x, 
			editorRect.y * percentageRect.y,
			editorRect.width * percentageRect.width, 
			editorRect.height * percentageRect.height
		);

		// Generates view's GUI Layout
		GUI.Box(viewRect, viewTitle);
		GUILayout.BeginArea(viewRect);
		CreateAreaContent();
		GUILayout.EndArea();

		ProcessEvents(e);
	}

	public virtual void ProcessEvents(Event e) {
		if (viewRect.Contains(e.mousePosition)) {
			Debug.Log("Inside: " + viewTitle);

			switch (e.button) {
			case 0:
				switch (e.type) {
				case EventType.MouseDown:
					Debug.Log("Left Click: Mouse Down on " +  viewTitle);
					break;
				case EventType.MouseDrag:
					Debug.Log("Left Click: Mouse Drag on " +  viewTitle);
					break;
				case EventType.MouseUp:
					Debug.Log("Left Click: Mouse Up on " +  viewTitle);
					break;
				}
				break;
			case 1:
				switch (e.type) {
				case EventType.MouseDown:
					Debug.Log("Right Click: Mouse Down on " +  viewTitle);
					ProcessContextMenu(e);
					break;
				}
				break;
			default:
				Debug.Log("Something went wrong with KeyCode");
				break;
			}
		}
	}
	protected void ProcessContextMenu(Event e) {
		menu = new GenericMenu();
		foreach (var option in contextMenuOptions) {
			menu.AddItem(new GUIContent(option.Key), false, option.Value, option.Key);
		}
		menu.ShowAsContext();
		e.Use();
	}
	#endregion

	#region Main Methods
	protected void GetEditorSkin() {}
	protected virtual void CreateAreaContent() {}
	#endregion
}