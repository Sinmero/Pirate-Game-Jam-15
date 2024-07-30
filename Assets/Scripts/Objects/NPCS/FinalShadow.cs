using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FinalShadow : NPC
{
    [SerializeField] IntakeMachine _finalIntakeMachine;


    public override void Init()
    {
        base.Init();

        _finalIntakeMachine.onStartedGettingResource += FinalDialogue;

        var startDialogue = new List<string>(){
            "Finish what we started.",
            "Succeed where we failed."
        };

        dialoguesDictionary["startDialogue"] = startDialogue;
        _dialoguesList = startDialogue;
    }



    public void FinalDialogue() {
        var finalDialogue = new List<string>(){
            "<color=#FF0000>Finally!</color>"
        };

        dialoguesDictionary.Add("finalDialogue", finalDialogue);
        _dialoguesList = finalDialogue;
    }
}
