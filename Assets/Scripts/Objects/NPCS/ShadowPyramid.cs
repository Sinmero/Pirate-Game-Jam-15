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
            "Did you know? This place used to be quite a marvel of engineering.",
            "It housed the best technology and brightest minds that humanity could offer.",
            "Then came the dust storms, covering the whole planet in shadow and creeping corrosion.",
            "Even people, if exposed long enough, will start to transform.|Pause(0,5)| Changing shape...",
            "I haven't seen the sunlight for so long, I'm unsure what it is like anymore.",
            "...",
            "Have my knowledge. It might come in handy.",
            "I don't have much use for it anymore.{GainKnowledge}{SecondDialogue}"
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
