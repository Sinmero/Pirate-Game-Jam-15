using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    public string _interactableName = "Generic Name";
    public string _actionName = "interact with";
    [HideInInspector] public string _interactKey;
    private BoxCollider2D _boxCollider2D;
    public float _interactTime = 1;


    void Start()
    {
        Init();
    }



    public virtual void Interact(Interactor interactor)
    {
        if (Input.GetKeyDown(Controls.keys._interact))
        {
            GameplayLogger.instance.Log($"{interactor._parentGO.name} interacted with {_interactableName}", this);
            interactor._midScreenText.text = "";
            OnInteract(interactor);
        }
    }



    public virtual void InteractSecondary(Interactor interactor) {
        if(Input.GetKeyDown(Controls.keys._interactSecondary)) {
            GameplayLogger.instance.Log($"{interactor._parentGO.name} secondary interacted with {_interactableName}", this);
            OnInteractSecondary(interactor);
        }
    }


    private Coroutine _timer;
    public virtual void InteractHold(Interactor interactor) {
        if(Input.GetKeyDown(Controls.keys._interactHold)) {
            GameplayLogger.instance.Log($"{interactor._parentGO.name} holding interaction with {_interactableName}", this);
            OnHoldStart(interactor);
            _timer = StartCoroutine(HoldTimer(interactor));
        }
        if(Input.GetKeyUp(Controls.keys._interactHold)) {
            GameplayLogger.instance.Log($"{interactor._parentGO.name} released interaction with {_interactableName}", this);
            StopCoroutine(_timer);
            OnHoldRelease();
        }
    }



    public IEnumerator HoldTimer(Interactor interactor) {
        yield return new WaitForSeconds(_interactTime);
        OnInteractHold(interactor);
    }



    public virtual void OnInteract(Interactor interactor) { }



    public virtual void OnInteractSecondary(Interactor interactor) { }



    public virtual void OnInteractHold(Interactor interactor) { }



    public virtual void OnHoldStart(Interactor interactor) { }



    public virtual void OnHoldRelease() { }



    public virtual void OnEnter(Interactor interactor)
    {
        interactor._midScreenText.text = GetPopupMessage();
        PhysicsLogger.instance.Log($"{name} trigger enter", this);
    }



    public virtual void OnLeave(Interactor interactor)
    {
        interactor._interactableList.Remove(this);
        if (interactor._interactableList.Count == 0)
        {
            interactor._midScreenText.text = "";
        }
        else
        {
            interactor._midScreenText.text = GetPopupMessage();
        }
    }



    public virtual string GetPopupMessage()
    {
        return $"Press {_interactKey} to {_actionName} {_interactableName}";
    }



    public virtual void Init()
    {
        _interactKey = Controls.keys._interact.ToString();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.isTrigger = true;
    }
}
