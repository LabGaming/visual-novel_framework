﻿using UnityEngine;
using UnityEngine.UI;

public class MessageBoxHud : MonoBehaviour
{
    [SerializeField]
    private ButtonTextHandler _okButton;
    [SerializeField]
    private Image _characterPortrait;
    [SerializeField]
    private Text _characterName;
    [SerializeField]
    private Text _sayingText;
    [SerializeField]
    private OptionsHandler _optionsHolder;

    private int _dialogId;
    private DialogManager _dialogManager;

    public void Construct(int dialogId, DialogManager dialogManager)
    {
        _dialogId = dialogId;
        _dialogManager = dialogManager;
        _okButton.SetText(EButtonText.OKAY);
    }

    //coming form button
    public void OkayPressed()
    {
        _dialogManager.OkayPressed(_dialogId);
    }

    //coming form button
    public void BackPressed()
    {
        _dialogManager.BackPressed(_dialogId);
    }

    public void SetData(BaseDialogNode dialogNode)
    {
        if(dialogNode == null)
            DialogComplete();
        else if(dialogNode is DialogStartNode)
            SetAsDialogStartNode((DialogStartNode) dialogNode);
        else if(dialogNode is DialogNode)
            SetAsDialogNode((DialogNode) dialogNode);
        else if(dialogNode is DialogMultiOptionsNode)
            SetAsMultiOptionsNode((DialogMultiOptionsNode) dialogNode);
        else
            Debug.LogError("Wrong Dialog type Sent Here");
    }

    private void DialogComplete()
    {
        _dialogManager.RemoveMessageBox(_dialogId);
        DestroyObject(gameObject);
    }

    private void SetAsDialogNode(DialogNode dialogNode)
    {
        _okButton.ShowButton(true);
        _okButton.SetText(dialogNode.IsNextAvailable() ? EButtonText.NEXT : EButtonText.OKAY);

        _characterPortrait.sprite = dialogNode.SayingCharacterPotrait;
        _characterName.text = dialogNode.SayingCharacterName;
        _sayingText.text = dialogNode.WhatTheCharacterSays;
    }

    private void SetAsDialogStartNode(DialogStartNode dialogStartNode)
    {
        _okButton.ShowButton(true);
        _okButton.SetText(dialogStartNode.IsNextAvailable() ? EButtonText.NEXT : EButtonText.OKAY);

        _characterPortrait.sprite = dialogStartNode.SayingCharacterPotrait;
        _characterName.text = dialogStartNode.SayingCharacterName;
        _sayingText.text = dialogStartNode.WhatTheCharacterSays;
    }


    private void SetAsMultiOptionsNode(DialogMultiOptionsNode dialogNode)
    {
        _okButton.ShowButton(false);

        _characterPortrait.sprite = dialogNode.SayingCharacterPotrait;
        _characterName.text = dialogNode.SayingCharacterName;
        _sayingText.text = dialogNode.WhatTheCharacterSays;

        _optionsHolder.CreateOptions(dialogNode.GetAllOptions(), OptionSelected);
        GrowMessageBox(dialogNode.GetAllOptions().Count);
    }

    private void GrowMessageBox(int count)
    {
        Vector2 size = GetComponent<RectTransform>().sizeDelta;
        size.y += (count * _optionsHolder.CellHeight());
        GetComponent<RectTransform>().sizeDelta = size;
    }

    private void OptionSelected(int optionSelected)
    {        
        _dialogManager.OptionSelected(_dialogId, optionSelected);
    }
}
