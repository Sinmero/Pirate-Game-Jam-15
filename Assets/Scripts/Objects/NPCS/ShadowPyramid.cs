using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPyramid : NPC
{
    [SerializeField] private GameObject _knowledgeGO;


    public override void Init()
    {
        base.Init();
        var startDialogue = new List<string>() {
            "This place used to be quite a marvel of engineering, you know?",
            "The best technology humanity could offer. The brightest of minds working in here.",
            "Then the dust storms came, covering the whole planet in shadow.",
            "Slowly corroding this place.",
            "Even people, during long contact with it start to transform.|Pause(0,5)| Changing shape.",
            "I havent seen the sunlight for so long, I am not even sure what it is like anymore.",
            "...",
            "Have my knowledge. It might come handy.",
            "I don't have much use for it anymore anyway.{GainKnowledge}"
        };

        dialoguesDictionary["startDialogue"] = startDialogue;
        _dialoguesList = startDialogue;
    }



    public void GainKnowledge() {
        _knowledgeGO.SetActive(true);
    }
}
