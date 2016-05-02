using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public static class EE_NodeMenus {
	[MenuItem("Window/EventEditor")]

	public static void InitNodeEditor() {
		EE_MainWindow.InitEditorWindow();
	}
}
