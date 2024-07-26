using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disconnected : NodeState
{
    public Disconnected(ConnectionNode parentConnectionNode) : base(parentConnectionNode)
    {
        _parentConnectionNode = parentConnectionNode;
    }



    public override void OnClick()
    {
        base.OnClick();
        if (ConnectNodes.instance._prevNodeState == null)
        {
            Linked linked = new Linked(_parentConnectionNode);
            // ConnectNodes.instance._prevNodeState = linked;
            _parentConnectionNode.ChangeNodeState(linked);
        }
    }
}
