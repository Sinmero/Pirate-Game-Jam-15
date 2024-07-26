using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Machine : Interactable
{
    private BoxCollider2D _boxCollider2d;
    public delegate void OnResourceAmountChange(int rNum);
    public event OnResourceAmountChange onResourceAmountChange;
    public delegate void OnResourceTypeChange();
    public event OnResourceTypeChange onResourceTypeChange;
    private string _rsrType = "";
    public string _resource {get{return _rsrType;} set{_rsrType = value; onResourceTypeChange?.Invoke();}}
    private int _rsr = 0;
    public int _resourceAmount {get{return _rsr;} set{_rsr = value; onResourceAmountChange?.Invoke(_rsr);}}
    [SerializeField] private GameObject _uiGO;

    void Start()
    {
        _boxCollider2d = GetComponent<BoxCollider2D>();
        _boxCollider2d.isTrigger = true;
        OnStart();
    }



    public virtual void OnStart() {

    }



    public override void OnEnter(Interactor interactor)
    {
        base.OnEnter(interactor);
        _uiGO?.SetActive(true);
    }



    public override void OnLeave(Interactor interactor)
    {
        base.OnLeave(interactor);
        _uiGO?.SetActive(false);
    }



    public virtual void NodeConnected() {

    }



    public virtual void NodeDisconnected() {
        
    }



    public virtual void OnConnectedResourceAmountChange(int amount) {

    }



    public virtual void OnConnectedResourceTypeChange() {

    }
}
