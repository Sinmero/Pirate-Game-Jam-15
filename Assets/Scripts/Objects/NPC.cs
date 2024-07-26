using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    //you can use commands to control the dialogue right throu the dialogue text
    //wrap command in "|" like this |reduceSpeed| to invoke methods from Speech class
    //wrap command in "{}" like this {doSomething} to invoke method from NPC and its children methods
    // !!! use ";" to split methods parameters NOT "," !!!
    // !!! parameters are MANDATORY for methods that take params otherwise you will get an error !!!



    // public string NPCName = "Generic NPC";
    public List<string> _dialoguesList = new List<string>();
    public float _dialogueSpeed = 0.1f;
    private int _timesInteracted = 0; //timesInteracted is used as index for preceeding dialogue
    public static NPC lastSpokeTo;
    public AudioClip _speechAudio;
    public delegate void OnDialogueEnd();
    public OnDialogueEnd onDialogueEnd;
    [HideInInspector] public Interactor _interactor;


    // public List<Sprite> talkingAnim = new List<Sprite>(), // not used for now
    // idleAnim = new List<Sprite>();


    public Dictionary<string, List<string>> dialoguesDictionary = new Dictionary<string, List<string>>(); //all character dialogues are here
    public List<List<string>> inspectorDialogueCreator = new List<List<string>>();



    private void Start()
    {
        Init();
    }



    public override void Init()
    {
        base.Init();
        onDialogueEnd += () => {Controls.UnlockControls();};
        var startDialogue = new List<string>{
        "Hello, i am " + _interactableName + " this is a test dialogue.",
        "You should not be seeing this."
        };

        if (_dialoguesList.Count != 0)
        {
            startDialogue = _dialoguesList; //if dialogue list has been set in inspector (npc dialogue set in unity inspector) then replace default start dialogue with it
        }

        _dialoguesList = startDialogue;
        dialoguesDictionary.Add("startDialogue", startDialogue); //setting up generic start dialogue
    }



    public override void OnInteract(Interactor interactor)
    {
        if (_interactor._dialogueUI._canProceed)
        {
            Controls.LockControls();
            base.OnInteract(interactor);
            if (lastSpokeTo != this)
            // { //not used
            //     lastSpokeTo = this;
            //     dialogueUI.clearDialoguePortaits();
            // }
            Speech.instance._thisType = this.GetType();
            Speech.instance._thisNPC = this;
            _interactor._dialogueUI.SetName(_interactableName);
            _interactor._dialogueUI.ToggleDialogueWindowOn();
            ProceedThrouDialogue();
        }
    }



    public void SetDialogueToDefault()
    { //sets dialogue to the start dialogue list
        _dialoguesList = dialoguesDictionary["startDialogue"];
        if (_timesInteracted > 0) // if used in the middle of dialogue will close the dialogue box the next interaction
        {
            _timesInteracted = 999;
        }
    }



    public void CloseWindowOnNextInteract()
    {
        _timesInteracted = 999;
    }



    public void ProceedThrouDialogue()
    {
        if (Speech.instance._isPrinting)
        {
            Speech.instance._textSpeed = 0.01f;
            Speech.instance._isPaused = false;
        }
        else
        {
            if (_timesInteracted <= _dialoguesList.Count - 1)
            {
                _interactor._dialogueUI.ClearDialogue();
                Speech.instance.PrintText(_dialoguesList[_timesInteracted], _interactor._dialogueUI._dialogueText, _dialogueSpeed, _speechAudio);
                _timesInteracted++;
            }
            else
            {
                CloseDialogueWindow();
                onDialogueEnd?.Invoke();
            }
        }
    }



    public void CloseDialogueWindow()
    {
        _timesInteracted = 0; //reset the dialogue so it would start from the beggining when interacted again
        _interactor._dialogueUI.ToggleDialogueWindowOff();
    }



    public void proceedThrouNewDialogue() //this is used when a dialogue option is pressed
    {
        _timesInteracted = 0;
        ProceedThrouDialogue();
    }



    public override void OnLeave(Interactor interactor)
    {
        base.OnLeave(interactor);
        Speech.instance.StopCoroutine();
        CloseDialogueWindow();
        onDialogueEnd?.Invoke(); //prevent the controls from getting locked
    }



    public override void OnEnter(Interactor interactor)
    {
        base.OnEnter(interactor);
        _interactor = interactor;
    }



    public void ChangeName(string str) {
        if(str == _interactableName) return;
        _interactableName = str;
        _interactor._dialogueUI.SetName(_interactableName);
    }




    // not used for now

    // public void talk()
    // {
    //     DialogueAnimationHandler.animationBoxList[1].thisAnimController.playRandom(talkingAnim);
    // }



    // public void idle()
    // {
    //     DialogueAnimationHandler.animationBoxList[1].thisAnimController.playForward(idleAnim);
    // }



    // public void increaseAnimationSpeed()
    // {
    //     DialogueAnimationHandler.animationBoxList[1].thisAnimController.increaseAnimationSpeed(5);
    // }



    // public void reduceAnimationSPeed()
    // {
    //     DialogueAnimationHandler.animationBoxList[1].thisAnimController.decreaseAnimationSpeed(5);
    // }

}
