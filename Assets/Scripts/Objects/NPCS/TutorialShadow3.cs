using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShadow3 : NPC
{
    [SerializeField] private IntakeMachine _doorIntake;
    [SerializeField] private GameObject _knowledgeGO;


    public override void Init()
    {
        base.Init();

        _doorIntake.onGotDesiredAmount += OnDoorOpen;

        var startDialogue = new List<string>() {
            "A piece of advice.",
            "The machines you build can alter the state of materials or even <color=#bfb7ff>change</color> them into something completely new.",
            "Knowledge is power. Don't worry, I will share mine with you.{GainKnowledge} Use it.",
            "Get closer to that sealed door. It will reveal its secrets",
            "Press <color=#FFFF00>'Tab'</color> key to toggle you knowledge screen.",
        };

        dialoguesDictionary["startDialogue"] = startDialogue;
        _dialoguesList = startDialogue;
    }



    public void OnDoorOpen () {
        var secondaryDialogue = new List<string>() {
            "Maybe not everything is lost after all..."
        };

        dialoguesDictionary.Add("secondaryDialogue", secondaryDialogue);
        _dialoguesList = secondaryDialogue;
    }



    public void GainKnowledge() {
        _knowledgeGO.SetActive(true);
    }
}
