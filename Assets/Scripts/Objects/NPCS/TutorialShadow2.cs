using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShadow2 : NPC
{
    [SerializeField] private Scrap _scrap;
    [SerializeField] private GameObject _otherShadow;
    [SerializeField] TriggerZone _triggerZone;

    public override void Init()
    {
        base.Init();



        var startDialogue = new List<string>() {
            "So you made it in here? |Pause(0,3)|Good.",
            "Did they tell you what this facility was built for?",
            "I thought so.",
            "See that heap of metal to the right of you? It used to be a pump machine but now it is junk.",
            "I cannot do much in my current 'form' |Pause(0,5)|but you on the other hand...",
            "There are still useful machinery parts in it.",
            "Get close to it and hold <color=#FFFF00>'F'</color><color=#FFFFFF> key to scrap it. I will wait you here.{SecondDialogue}"
        };

        dialoguesDictionary["startDialogue"] = startDialogue;
        _dialoguesList = startDialogue;

        _scrap.onScrap += OnScrap;
    }



    public void SecondDialogue() {
        var secondDialogue = new List<string>() {
            "Get close to it and hold <color=#FFFF00>'F'</color><color=#FFFFFF> key to scrap it. I will wait you here."
        };
        dialoguesDictionary.Add("secondDialogue", secondDialogue);
        _dialoguesList = secondDialogue;

        _triggerZone.gameObject.SetActive(true);
        
        _triggerZone.onTriggerEnter += OnPlayerTriggerEnter;
    }



    public void OnScrap() {
        var scrapDialogue = new List<string>() {
            "Yes! Now you should have enough materials to build your own machines.",
            "You can press the <color=#FFFF00>'I'</color><color=#FFFFFF> key to check materials you have.",
            "Why don't you build a new pump machine. It has to be placed on a transport node. That green thing sticking out of the floor.",
            "Those nodes used to deliver all kinds of materials throughout the facility. And with a help of a pump machine you can access them!",
            "Press <color=#FFFF00>'B'</color><color=#FFFFFF> to toggle your build menu. You can select a machine by pressing number keys.",
            "Press <color=#FFFF00>'1'</color><color=#FFFFFF> to select the pump machine, then get near the transport node and press <color=#FFFF00>'Space'</color><color=#FFFFFF> key to place the machine.",
            "You will notice that it has a circular node above with a symbol inscribed on it. This is an output node. Pump machines only have output nodes but other will also have input nodes.",
            "You can <color=#FFFF00>'Left click'</color><color=#FFFFFF> nodes to connect them and transfer materials between machines.",
            "Empty input nodes are always blue and output are green.{OnScrapSecondary}"
        };
        dialoguesDictionary.Add("scrapDialogue", scrapDialogue);
        _dialoguesList = scrapDialogue;
    }



    public void OnScrapSecondary() {
        var onScrapSecondary = new List<string>() {
            "Press <color=#FFFF00>'B'</color><color=#FFFFFF> to toggle your build menu. You can select a machine by pressing number keys.",
            "Press <color=#FFFF00>'1'</color><color=#FFFFFF> to select the pump machine, then get near the transport node and press <color=#FFFF00>'Space'</color><color=#FFFFFF> key to place the machine.",
            "You will notice that it has a circular node above with a symbol inscribed on it. This is the output node. Pump machines only have an output nodes but other will also have input nodes.",
            "You can <color=#FFFF00>'Left click'</color><color=#FFFFFF> nodes to connect them and transfer materials between machines.",
            "Empty input nodes are always blue and output are green"
        };
        dialoguesDictionary.Add("onScrapSecondary", onScrapSecondary);
        _dialoguesList = onScrapSecondary;
    }



    public void OnPlayerTriggerEnter() {

        _otherShadow.SetActive(true);

        Destroy(_triggerZone.gameObject);

        gameObject.SetActive(false);
    }
}
