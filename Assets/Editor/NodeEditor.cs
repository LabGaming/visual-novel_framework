using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

 
public class EventEditor: EditorWindow {
   
	Rect event1;
	Rect event2;
   
    [MenuItem("Window/Event editor")]
    static void ShowEditor() {
		EventEditor editor = EditorWindow.GetWindow<EventEditor>();
        editor.Init();
    }
   
    public void Init() {
		event1 = new Rect(10, 10, 100, 100);  
		event2 = new Rect(210, 210, 100, 100);
    }
   
    void OnGUI() {
		DrawNodeCurve(event1, event2);
       
        BeginWindows();
		event1 = GUI.Window(1, event1, DrawNodeWindow, "Event 1");   
		event2 = GUI.Window(2, event2, DrawNodeWindow, "Event 2");
        EndWindows();
    }
   
    void DrawNodeWindow(int id) {
        GUI.DragWindow();
    }
   
    void DrawNodeCurve(Rect start, Rect end) {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}