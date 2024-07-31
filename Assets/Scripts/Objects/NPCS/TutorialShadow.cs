using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShadow : NPC
{

    public override void Init()
    {
        base.Init();

        _dialoguesList = new List<string>() {
            "Ah, a new face.",
            "You look disoriented, friend. Did something happen on your journey here?",
            "Considering what's going on outside, it is no wonder.",
            "The ledge? You will need to double jump to reach it.",
            "Press <color=#FFFF00>'W'</color><color=#FFFFFF> key while in mid air to double-jump.</color>{SecondDialogue}",
        };
    }



    public void SecondDialogue() {
        _dialoguesList = new List<string>() {
            "Press <color=#FFFF00>'W'</color><color=#FFFFFF> key while in mid air to double-jump.</color>",
        };
    }
}
