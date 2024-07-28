using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Machine : Interactable
{
    private BoxCollider2D _boxCollider2d;
    public delegate void OnResourceAmountChange(int rNum);
    public event OnResourceAmountChange onResourceAmountChange;
    public delegate void OnResourceTypeChange();
    public event OnResourceTypeChange onResourceTypeChange;
    public event OnResourceTypeChange onResourceStateChange;

    public List<InputNode> _inputNodeList = new List<InputNode>();

    private string _rsrType = "";
    public string _resource {get{return _rsrType;} set{_rsrType = value; onResourceTypeChange?.Invoke();}}
    private string _rsrState = "";
    public string _resourceState {get {return _rsrState;} set{_rsrState = value; onResourceStateChange?.Invoke();}}
    private int _rsr = 0;
    public int _resourceAmount {get{return _rsr;} set{_rsr = value; onResourceAmountChange?.Invoke(_rsr);}}
    [SerializeField] private GameObject _uiGO;
    public DissolveAnimation _dissolveAnimation;



    void Start()
    {
        _boxCollider2d = GetComponent<BoxCollider2D>();
        // _boxCollider2d.isTrigger = true;
        OnStart();
    }



    public virtual void OnStart() {

    }



    public override void OnEnter(Interactor interactor)
    {
        base.OnEnter(interactor);
        if(_uiGO != null) _uiGO.SetActive(true);
    }



    public override void OnLeave(Interactor interactor)
    {
        base.OnLeave(interactor);
        if(_uiGO != null) _uiGO.SetActive(false);
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
