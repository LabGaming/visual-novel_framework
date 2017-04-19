using System;
using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using UnityEditor;
using UnityEngine;

[Node(false, "Dialog/Dialog With Options Node", new Type[]{typeof(DialogNodeCanvas)})]
public class DialogMultiOptionsNode : BaseDialogNode
{
    private const string Id = "multiOptionDialogNode";
    public override string GetID { get { return Id; } }
    public override Type GetObjectType { get { return typeof(DialogMultiOptionsNode); } }

    private const int StartValue = 222;
    private const int SizeValue = 22;

    [SerializeField]
    List<DataHolderForOption> _options;

    public override Node Create(Vector2 pos)
    {
        DialogMultiOptionsNode node = CreateInstance<DialogMultiOptionsNode>();

		node.rect = new Rect(pos.x, pos.y, 300, 350);
        node.name = "Dailog with Options Node";

        //Previous Node Connections
        node.CreateInput("Previous Node", "DialogForward", NodeSide.Left, 30);
        node.CreateOutput("Back Node", "DialogBack", NodeSide.Left, 50);

        ////Next Node to go to
        //node.CreateOutput("Next Node", "DialogForward", NodeSide.Right, 30);

        node.SayingCharacterName = "Morgen Freeman";
		node.WhatTheCharacterSays = "I'm GOD";
		node.SayingBackground = null;
        node.SayingCharacterPotrait = null;

        node._options = new List<DataHolderForOption>();

        node.AddNewOption();
        
        return node;
    }

    protected internal override void NodeGUI()
    {
		EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
        GUILayout.BeginHorizontal();

        SayingCharacterName = EditorGUILayout.TextField("Character Name", SayingCharacterName);

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        WhatTheCharacterSays = EditorGUILayout.TextArea(WhatTheCharacterSays, GUILayout.Height(100));

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        SayingCharacterPotrait = EditorGUILayout.ObjectField("Potrait", SayingCharacterPotrait,
			typeof(Sprite), false) as Sprite;

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
              
		SayingBackground = EditorGUILayout.ObjectField("Background", SayingBackground,
            typeof(Sprite), false) as Sprite;

        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        DrawOptions();

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Space(5);
        if(GUILayout.Button("Add New Option"))
        {
            AddNewOption();
            IssueEditorCallBacks();
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
    }

	private void DrawOptions()
	{
		EditorGUILayout.BeginVertical();
		foreach (var option in _options.ToList()) {
			GUILayout.BeginVertical();
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Outputs.IndexOf(option.NodeOutput) + ".", GUILayout.MaxWidth(15));
			option.OptionDisplay = EditorGUILayout.TextArea(option.OptionDisplay, GUILayout.MinWidth(80));
			OutputKnob (Outputs.IndexOf(option.NodeOutput));
			if (GUILayout.Button("‒", GUILayout.Width(20)))
			{
				_options.Remove(option);
				option.NodeOutput.Delete();
				rect = new Rect(rect.x, rect.y, rect.width, rect.height - SizeValue);
			}

			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.Space(4);
		}
		GUILayout.EndVertical();
	}

    private void AddNewOption()
    {
        DataHolderForOption option = new DataHolderForOption {OptionDisplay = "Write Here"};
		option.NodeOutput = CreateOutput("Next Node", "DialogForward", NodeSide.Right,
            StartValue + _options.Count * SizeValue);        
        rect = new Rect(rect.x, rect.y, rect.width, rect.height + SizeValue);
        _options.Add(option);
    }

    //For Resolving the Type Mismatch Issue
    private void IssueEditorCallBacks()
    {
        DataHolderForOption option = _options.Last();
		NodeEditorCallbacks.IssueOnAddNodeKnob(option.NodeOutput);
    }

    public override BaseDialogNode Input(int inputValue)
    {
        switch (inputValue)
        {
            case (int)EDialogInputValue.Next:
                if (Outputs[1].GetNodeAcrossConnection() != default(Node))
                    return Outputs[1].GetNodeAcrossConnection() as BaseDialogNode;
                break;
            case (int)EDialogInputValue.Back:
                if(Outputs[0].GetNodeAcrossConnection() != default(Node))
                    return Outputs[0].GetNodeAcrossConnection() as BaseDialogNode;
                break;
            default:
                if(_options[inputValue].NodeOutput.GetNodeAcrossConnection() != default(Node))
					return _options[inputValue].NodeOutput.GetNodeAcrossConnection() as BaseDialogNode;
                break;
        }
        return null;
    }

    public override bool IsBackAvailable()
    {
        return Outputs[0].GetNodeAcrossConnection() != default(Node);
    }

    public override bool IsNextAvailable()
    {
        return false;
    }


    [Serializable]
    class DataHolderForOption
    {
        public string OptionDisplay;
        public NodeOutput NodeOutput;                
    }

    public List<string> GetAllOptions()
    {
        return _options.Select(option => option.OptionDisplay).ToList();
    }
}
