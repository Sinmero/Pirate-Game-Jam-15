using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPyramid : NPC
{
    [SerializeField] private GameObject _knowledgeGO;
    [SerializeField] private IntakeMachine _intakeMachine;


    public override void Init()
    {
        base.Init();

        _intakeMachine.onStartedGettingResource += FinalDialogue;

        var startDialogue = new List<string>() {
            "This place used to be quite a marvel of engineering, you know?",
            "The best technology humanity could offer. The brightest of minds working in here.",
            "Then the dust storms came, covering the whole planet in shadow, slowly corroding this place.",
            "Even people, if explosed long enough will start to transform.|Pause(0,5)| Changing shape.",
            "I havent seen the sunlight for so long, I am not even sure what it is like anymore.",
            "...",
            "Have my knowledge. It might come handy.",
            "I don't have much use for it anymore anyway.{GainKnowledge}{SecondDialogue}"
        };

        dialoguesDictionary["startDialogue"] = startDialogue;
        _dialoguesList = startDialogue;
    }



    public void GainKnowledge() {
        _knowledgeGO.SetActive(true);
    }



    public void SecondDialogue () {
        var secondDialogue = new List<string>() {
            "..."
        };

        dialoguesDictionary.Add("secondDialogue", secondDialogue);
        _dialoguesList = secondDialogue;
    }



    public void FinalDialogue() {
        gameObject.SetActive(false);
    }
}
