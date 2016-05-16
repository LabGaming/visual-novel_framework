using UnityEngine;
using UnityEditor;
using System.Collections;

public class EE_PopUpWindow : EditorWindow {
	#region Variables
	static EE_PopUpWindow currentPopUp;
	string wantedName = "Enter a Name:";
	#endregion

	#region Main Methods
	public static void InitNodePopUp() {
		currentPopUp = (EE_PopUpWindow) EditorWindow.GetWindow<EE_PopUpWindow> ();
		currentPopUp.titleContent.text = "PopUp";
	}
	void OnGUI() {
		GUILayout.Space(20);
		GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			// Title and input Field 
			GUILayout.BeginVertical();
				renderPopUpContent();
			GUILayout.EndVertical();
			GUILayout.Space(20);
		GUILayout.EndHorizontal();
		GUILayout.Space(20);
	}
	#endregion

	#region Utility Methods
	protected virtual void renderPopUpContent(){
		EditorGUILayout.LabelField("Create New Graph:", EditorStyles.boldLabel);
		wantedName = EditorGUILayout.TextField("Enter graph name ...", wantedName);
		GUILayout.Space(10);
		// Buttons
		GUILayout.BeginHorizontal();
			if(GUILayout.Button("Create Graph", GUILayout.Height(40))){
				currentPopUp.Close();
				EE_NodeUtils.CreateNewGraph(wantedName);
			}
			if(GUILayout.Button("Cancel", GUILayout.Height(40))){
				currentPopUp.Close();
			}
		GUILayout.EndHorizontal();
	}
	#endregion
}
