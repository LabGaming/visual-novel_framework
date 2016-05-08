using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class EE_MainWindow : EditorWindow {

	#region Variables
	public static EE_MainWindow currentWindow;

	public EE_NodePropertyView propertyView;
	public EE_NodeWorkView workView;

	public EE_NodeGraph currentGraph;

	public float viewPercentange = .75f;
	#endregion

	#region Main Methods
	public static void InitEditorWindow() {
		currentWindow = (EE_MainWindow) EditorWindow.GetWindow<EE_MainWindow>();
		currentWindow.titleContent.text = "Event Editor";

		CreateViews();
	}

	void OnGUI() {
		if (propertyView == null || workView == null) {
			CreateViews();
			return;
		}

		Event e = Event.current;
		UpdateViews(e);

		Repaint();
	}
	#endregion

	#region Utility Methods
	static void CreateViews() {
		if (currentWindow != null) {
			currentWindow.propertyView = new EE_NodePropertyView();
			currentWindow.workView = new EE_NodeWorkView();
		} else {
			currentWindow = (EE_MainWindow) EditorWindow.GetWindow<EE_MainWindow>();
		}
	}

	protected void UpdateViews(Event e){
		workView.UpdateView(position, new Rect(0f, 0f, viewPercentange, 1f), e, currentGraph);
		propertyView.UpdateView(
			new Rect(position.width, position.y, position.width, position.height),
			new Rect(viewPercentange, 0f, 1f - viewPercentange, 1f),
			e,
			currentGraph
		);
	}
	#endregion
}
