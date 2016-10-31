using UnityEngine;
using UnityEngine.UI;

public class MessageBoxHud : MonoBehaviour
{
    [SerializeField]
	private ButtonTextHandler _okButton;
    [SerializeField]
    private Image _background;
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
        _okButton.SetText(EButtonText.END);
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
        if (dialogNode == null) {
            DialogComplete();
        } else {
			SetAsBaseDialogNode(dialogNode);
			if (dialogNode is DialogMultiOptionsNode)
				SetAsMultiOptionsNode((DialogMultiOptionsNode) dialogNode);
		}
    }

    private void DialogComplete()
    {
        _dialogManager.RemoveMessageBox(_dialogId);
        DestroyObject(gameObject);
    }

    private void SetAsBaseDialogNode(BaseDialogNode dialogNode)
    {
        _okButton.ShowButton(true);
        _okButton.SetText(dialogNode.IsNextAvailable() ? EButtonText.NEXT : EButtonText.END);

		_background.sprite = dialogNode.SayingBackground;
        _characterPortrait.sprite = dialogNode.SayingCharacterPotrait;
        _characterName.text = dialogNode.SayingCharacterName;
        _sayingText.text = dialogNode.WhatTheCharacterSays;
    }


    private void SetAsMultiOptionsNode(DialogMultiOptionsNode dialogNode)
    {
        _okButton.ShowButton(false);
        _optionsHolder.CreateOptions(dialogNode.GetAllOptions(), OptionSelected);
    }

    private void OptionSelected(int optionSelected)
    {        
        _dialogManager.OptionSelected(_dialogId, optionSelected);
    }
}
