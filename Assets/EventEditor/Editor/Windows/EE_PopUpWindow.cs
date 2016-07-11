using UnityEngine;
using UnityEditor;
using System.Collections;

public enum PopUpType
{
	CreateNode, 
	AddOption
}

public class EE_PopUpWindow : EditorWindow {
	#region Variables
	static EE_PopUpWindow currentPopUp;
	private EE_NodeSimpleDialog nodeToModify;
	public string name = "";
	public string content = "";
	private PopUpType popUpType;
	#endregion

	#region Main Methods
	public static void InitNodePopUp(PopUpType type) {
		currentPopUp = (EE_PopUpWindow) EditorWindow.GetWindow<EE_PopUpWindow> ();
		currentPopUp.popUpType = type;
		currentPopUp.titleContent.text = "PopUp";
	}
	public static void InitNodePopUp(PopUpType type, EE_NodeSimpleDialog node) {
		InitNodePopUp(type);
		currentPopUp.nodeToModify = node;
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
		string labelField = "";
		string nameTextField = "Name";
		string contentTextField = "";
		string buttonName = "";
		switch (popUpType) {
		case PopUpType.CreateNode:
			labelField = "Create Graph :";
			buttonName = "Create Graph";
			break;
		case PopUpType.AddOption:
			labelField = "Add Option for " + nodeToModify.name + " :";
			contentTextField = "Options Content";
			buttonName = "Add Option";
			break;
		default:
			break;
		}
		EditorGUILayout.LabelField(labelField, EditorStyles.boldLabel);
		name = EditorGUILayout.TextField(nameTextField, name);
		switch (popUpType) {
		case PopUpType.AddOption:
			content = EditorGUILayout.TextField(contentTextField, content);
			break;
		default:
			break;
		}
		GUILayout.Space(10);
		// Buttons
		GUILayout.BeginHorizontal();
			if(GUILayout.Button(buttonName, GUILayout.Height(40))){
				currentPopUp.Close();
				switch (popUpType) {
				case PopUpType.CreateNode:
					EE_NodeUtils.CreateNewGraph(name);
					break;
				case PopUpType.AddOption:
					EE_NodeUtils.AddOptionToNode(name, content, currentPopUp.nodeToModify);
					break;
				default:
					break;
				}
			}
			if(GUILayout.Button("Cancel", GUILayout.Height(40))){
				currentPopUp.Close();
			}
		GUILayout.EndHorizontal();
	}
	#endregion
}
