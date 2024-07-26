using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI
    _NPCName,
    _dialogueText;
    [SerializeField] private Image _dialogueWindow;
    [HideInInspector] public bool _canProceed = true;



    void Start()
    {
        if (_NPCName == null) SystemLogger.instance.Log($"_NPCName is null. Put NPC Name reference in here", this);
        if (_dialogueText == null) SystemLogger.instance.Log($"_dialogueText is null. Put Dialogue Text reference in here", this);
    }



    public void ClearAll()
    {
        _NPCName.text = "";
        _dialogueText.text = "";
    }



    public void ClearDialogue()
    {
        _dialogueText.text = "";
    }



    public void ToggleDialogueWindowOff()
    {
        if (_dialogueWindow.IsActive()) _dialogueWindow.gameObject.SetActive(false);
    }



    public void ToggleDialogueWindowOn()
    {
        if (!_dialogueWindow.IsActive()) _dialogueWindow.gameObject.SetActive(true);
    }



    public void SetName(string str) {
        _NPCName.text = str;
    }
}
