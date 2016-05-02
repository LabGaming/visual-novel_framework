using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

[Serializable]
public class EE_ViewBase {
	#region Public Variables
	public string viewTitle;
	public Rect viewRect;
	#endregion

	#region Protected Variables
	protected GUISkin viewSkin;
	#endregion

	#region Constructor
	public EE_ViewBase(string title) {
		viewTitle = title;
	}
	#endregion

	#region Main Methods
	public virtual void UpdateView(Rect editorRect, Rect percentageRect, Event e, EE_NodeGraph currentGraph) {
		viewRect = new Rect(
			editorRect.x * percentageRect.x, 
			editorRect.y * percentageRect.y,
			editorRect.width * percentageRect.width, 
			editorRect.height * percentageRect.height
		);

		if (currentGraph != null) {
			currentGraph.UpdateGraphGUI(e, viewRect);
		}

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
				default:
					Debug.Log("Something went wrong with KeyCode");
					break;
				}
				break;
			case 1:
				switch (e.type) {
				case EventType.MouseDown:
					Debug.Log("Right Click: Mouse Down on " +  viewTitle);
					break;
				default:
					Debug.Log("Something went wrong with KeyCode");
					break;
				}
				break;
			default:
				Debug.Log("Something went wrong with KeyCode");
				break;
			}
		}
	}
	#endregion

	#region Main Methods
	protected void GetEditorSkin() {}
	protected virtual void CreateAreaContent() {}
	#endregion
}
