using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowStairs : NPC
{
    [SerializeField] private GameObject _knowledgeGO;
    [SerializeField] private IntakeMachine _intakeMachine;
    

    public override void Init()
    {
        base.Init();

        _intakeMachine.onStartedGettingResource += FinalDialogue;

        var startDialogue = new List<string>() {
            "Welcome to our home.",
            "I can't believe they sent someone to deal with this mess after all this time.",
            "I suppose you want to know the reason why this facility is in ruins.",
            "No... it was not exactly the dust storm.",
            "They did not inform you of our work, did they?",
            "Our original goal was to produce various exotic matters. One day, we discovered something really special.",
            "Something that is no less than a miracle.",
            "A way to peak beyond the veil of existence!",
            "Needless to say, we were extremely cautious.|Pause(0,3)| But our superiors pushed the matter, despite our protests.",
            "In our haste, we made a mistake. We paid with our lives... our very reality.",
            "I would urge you not to repeat the process, but I know that you won't listen.",
            "Have my knowledge and, please, leave me be. I will not be responsible for what happens next.{GainKnowledge}{SecondDialogue}"
        };

        dialoguesDictionary["startDialogue"] = startDialogue;
        _dialoguesList = startDialogue;
    }



    public void SecondDialogue() {
        var secondDialogue = new List<string>() {
            "I would like to be alone now."
        };

        dialoguesDictionary.Add("secondDialogue", secondDialogue);
        _dialoguesList = secondDialogue;
    }



    public void GainKnowledge() {
        _knowledgeGO.SetActive(true);
    }



    public void FinalDialogue() {
        var finalDialogue = new List<string>() {
            "<color=#FF0000>What have you done?!</color>"
        };

        dialoguesDictionary.Add("finalDialogue", finalDialogue);
        _dialoguesList = finalDialogue;
    }
}
