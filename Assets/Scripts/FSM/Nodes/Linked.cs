using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Linked : NodeState
{
    public Linked(ConnectionNode parentConnectionNode) : base(parentConnectionNode) {
        _parentConnectionNode = parentConnectionNode;
    }



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _parentConnectionNode.InstantiateLine();
        ConnectNodes.instance._prevNodeState = this;
        ConnectNodes.instance._currentLineRenderer = _parentConnectionNode._connectionLine;
        ConnectNodes.instance._startPos = _parentConnectionNode.transform.position;
        ConnectNodes.instance.onUpdate += ConnectNodes.instance.Connect;
    }



    public override void OnClick()
    {
        base.OnClick();
        ConnectNodes.instance.ClearLine(_parentConnectionNode._connectionLine); //clear line if clicked on itself
        ConnectNodes.instance.Clear();
        _parentConnectionNode.ChangeNodeState(new Disconnected(_parentConnectionNode));
    }



    public override void OnClickOther(NodeState otherNodeState)
    {
        base.OnClickOther(otherNodeState);
        if(otherNodeState._parentConnectionNode._nodeType == _parentConnectionNode._nodeType) { //cant connect with the same node type
            ConnectNodes.instance.ClearLine(_parentConnectionNode._connectionLine); //clear line if clicked on ourselves
            ConnectNodes.instance.Clear();
            SystemLogger.instance.Log($"Cant connect two nodes of the same type", _parentConnectionNode);

            _parentConnectionNode.ChangeNodeState(new Disconnected(_parentConnectionNode)); //clicked on the same type. Changing back to Disconnected state

            return;
        }

        otherNodeState._parentConnectionNode._otherConnectionNode = _parentConnectionNode;
        _parentConnectionNode._otherConnectionNode = otherNodeState._parentConnectionNode;
        otherNodeState._parentConnectionNode._connectionLine = _parentConnectionNode._connectionLine;

        ConnectNodes.instance.Clear();

        ConnectNodes.instance.ConnectLine(_parentConnectionNode.transform.position, otherNodeState._parentConnectionNode.transform.position, _parentConnectionNode._connectionLine); //make a line between nodes

        _parentConnectionNode.onConnect?.Invoke();
        otherNodeState._parentConnectionNode.onConnect?.Invoke();

        otherNodeState._parentConnectionNode.ChangeNodeState(new Connected(otherNodeState._parentConnectionNode));
        _parentConnectionNode.ChangeNodeState(new Connected(_parentConnectionNode));
    }
}
