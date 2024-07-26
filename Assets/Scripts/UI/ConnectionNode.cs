using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class ConnectionNode : MonoBehaviour, IClickable
{
    public string _nodeType;
    // private int _rsrAmount;
    // [HideInInspector] public int _resourceAmount {get{return _rsrAmount;} set{_rsrAmount = value; onAmountChange?.Invoke();}}
    private CircleCollider2D _circleCollider2D;
    public SpriteRenderer _icon;
    [HideInInspector] public  SpriteRenderer _circleSprite;
    public Machine _machine;


    private ConnectionNode _cnNode;
    public ConnectionNode _otherConnectionNode {get{return _cnNode;} set{if(value == null) onDisconnect?.Invoke(); _cnNode = value;}}

    public NodeState _currentNodeState;

    [SerializeField] private LineRenderer _lineRenderer;
    [HideInInspector] public LineRenderer _connectionLine;
    [HideInInspector] public Color _resourceColor;

    public delegate void Connect();
    public Connect onConnect;
    public Connect onDisconnect;
    public delegate void OnAmountChange(int amount);
    public OnAmountChange onAmountChange;



    private void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.isTrigger = true;
        _circleSprite = GetComponent<SpriteRenderer>();
        OnStart();
    }



    public virtual void OnStart()
    {
        SetNodeIcon();
        _currentNodeState = new Disconnected(this);
    }



    public void IOnClick()
    {
        SystemLogger.instance.Log($"Player clicked on {this}", this);
        OnNodeClick();
    }



    public virtual void OnNodeClick() {
        _currentNodeState.OnClick();
        
        OnClick();

        if(ConnectNodes.instance._prevNodeState != null && ConnectNodes.instance._prevNodeState != _currentNodeState) ConnectNodes.instance._prevNodeState.OnClickOther(_currentNodeState);
    }



    public virtual void OnClick(){}



    public void InstantiateLine() {
        _connectionLine = Instantiate(_lineRenderer, transform.position, quaternion.identity, transform);
    }



    public virtual void SetNodeIcon()
    {
        if (_machine._resource == "")
        {
            _icon.sprite = null;
            return;
        }
        SystemLogger.instance.Log($"Setting {this.name} icon", this);
        Sprite iconSprite = Items.instance._itemDictionary[_machine._resource]._icon;
        _circleSprite.color = Items.instance._itemDictionary[_machine._resource]._itemColor;
        _icon.sprite = iconSprite; //setting the sprite
    }



    public void ChangeNodeState(NodeState nodeState) {
        if(_currentNodeState != null) _currentNodeState.OnStateExit();
        SystemLogger.instance.Log($"Changing {this} state to {nodeState}", this);
        _currentNodeState = nodeState;
        _currentNodeState.OnStateEnter();
    }
}
