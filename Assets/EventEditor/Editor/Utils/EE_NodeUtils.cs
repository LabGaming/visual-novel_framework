using UnityEngine;
using UnityEditor;
using System.Collections;

public static class EE_NodeUtils {
	public static void CreateNewGraph(string wantedName) {
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

	public static void LoadGraph() {
		string graphPath = EditorUtility.OpenFilePanel("Load Graph",  Application.dataPath + "/EventEditor/Database/", "");
		graphPath = graphPath.Substring(Application.dataPath.Length - 6);
		EE_NodeGraph currentGraph = (EE_NodeGraph) AssetDatabase.LoadAssetAtPath(graphPath, typeof(EE_NodeGraph));
		if (currentGraph != null) {
			LoadGraphToMainWindow(currentGraph);
		} else {
			EditorUtility.DisplayDialog("Error Message", "Unable to Load selected Graph", "Sorry!");
		}

		Debug.Log(graphPath);
	}

	private static void LoadGraphToMainWindow(EE_NodeGraph currentGraph){
		EE_MainWindow currentWindow = (EE_MainWindow) EditorWindow.GetWindow<EE_MainWindow>();
		currentWindow.currentGraph = currentGraph;
	}
}
