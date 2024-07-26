using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{
    [HideInInspector] public CircleCollider2D _circleCollider2D;
    [HideInInspector] public List<Interactable> _interactableList = new List<Interactable>();
    [HideInInspector] public GameObject _parentGO;
    public TextMeshProUGUI _midScreenText;
    public DialogueUI _dialogueUI;


    void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _parentGO = transform.parent.gameObject;
        if(_midScreenText == null) SystemLogger.instance.Log($"_midScreenText is null. Make sure to put a TextMeshProUGUI reference here", this);
        if(_dialogueUI == null) SystemLogger.instance.Log($"_dialogueUI is null. Make sure to put a DialogueUI reference here", this);
    }



    private void OnTriggerEnter2D(Collider2D other)
    { //add interactable to the list on trigger enter
        Interactable interactable;
        interactable = other.GetComponent<Interactable>();
        if (interactable == null) return;
        _interactableList.Add(interactable);
        interactable.OnEnter(this);
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable;
        interactable = other.GetComponent<Interactable>();
        if(interactable != null) interactable.OnLeave(this);
    }



    private void Update()
    {
        if (_interactableList.Count == 0) return;
        _interactableList[_interactableList.Count - 1].Interact(this); //call OnInteract of the last object on the list
        _interactableList[_interactableList.Count - 1].InteractSecondary(this); //call OnInteract of the last object on the list
        _interactableList[_interactableList.Count - 1].InteractHold(this); //call OnInteract of the last object on the list
    }



    public virtual void OnHoldInteract(Interactable interactable) {

    }



    public virtual void OnHoldInterrupt(Interactable interactable) {
        
    }
}
