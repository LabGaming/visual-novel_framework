using UnityEngine;
using UnityEditor;
using System.Collections;

public static class EE_NodeUtils {
	#region Event Graph API
	public static void CreateNewGraph (string wantedName) {
		// Create EE_NodeGraph
		EE_NodeGraph currentGraph = (EE_NodeGraph) ScriptableObject.CreateInstance<EE_NodeGraph> ();
		currentGraph.graphName = wantedName;
		currentGraph.InitGraph();
		// Save EE_NodeGraph to the AssetsDatabase
		AssetDatabase.CreateAsset(currentGraph, "Assets/EventEditor/Database/" + wantedName + ".asset");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		// Attach EE_NodeGraph to EE_MainWindow
		LoadGraphToMainWindow(currentGraph);
	}

	public static void LoadGraph () {
		string graphPath = EditorUtility.OpenFilePanel("Load Graph",  Application.dataPath + "/EventEditor/Database/", "");
		graphPath = graphPath.Substring(Application.dataPath.Length - 6);
		EE_NodeGraph currentGraph = (EE_NodeGraph) AssetDatabase.LoadAssetAtPath(graphPath, typeof(EE_NodeGraph));

		if (currentGraph != null) {
			LoadGraphToMainWindow(currentGraph);
		} else {
			EditorUtility.DisplayDialog("Error Message", "Unable to Load selected Graph", "Damn it!");
		}
	}

	public static void CreateNode (NodeType nodeType, Vector2 mousePosition){
		EE_NodeGraph currentGraph = GetGraphToMainWindow();
		EE_NodeBase currentNode = null;

		switch (nodeType) {
		case NodeType.SimpleDialog:
			currentNode = (EE_NodeSimpleDialog) ScriptableObject.CreateInstance<EE_NodeSimpleDialog>();
			break;
		default:
			break;
		}

		if (currentNode != null) {
			currentNode.InitNode();
			currentNode.nodeRect.x = mousePosition.x;
			currentNode.nodeRect.y = mousePosition.y;
			currentNode.parentGraph = currentGraph;
			currentGraph.nodes.Add(currentNode);
			AssetDatabase.AddObjectToAsset(currentNode, currentGraph);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		} else {
			EditorUtility.DisplayDialog("Error Message", "Unable to Create Event Node", "Damn it!");
		}
	}

	public static void DeleteNode(EE_NodeBase node) {
		EE_NodeGraph currentGraph = GetGraphToMainWindow();
		currentGraph.nodes.Remove(node);
		GameObject.DestroyImmediate(node, true);
		AssetDatabase.Refresh();
	}
	#endregion

	private static void LoadGraphToMainWindow (EE_NodeGraph currentGraph){
		EE_MainWindow currentWindow = (EE_MainWindow) EditorWindow.GetWindow<EE_MainWindow>();
		currentWindow.currentGraph = currentGraph;
	}

	private static EE_NodeGraph GetGraphToMainWindow (){
		EE_MainWindow currentWindow = (EE_MainWindow) EditorWindow.GetWindow<EE_MainWindow>();
		return currentWindow.currentGraph;
	}
}
