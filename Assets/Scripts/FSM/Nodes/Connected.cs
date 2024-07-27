using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connected : NodeState
{
    public Connected(ConnectionNode parentConnectionNode) : base(parentConnectionNode) {
        _parentConnectionNode = parentConnectionNode;
    }



    public override void OnClick()
    {
        base.OnClick();

        ConnectNodes.instance.ClearLine(_parentConnectionNode._connectionLine);
        _parentConnectionNode._otherConnectionNode._connectionLine = null;
        _parentConnectionNode._otherConnectionNode._otherConnectionNode = null;
        _parentConnectionNode._otherConnectionNode.ChangeNodeState(new Disconnected(_parentConnectionNode._otherConnectionNode));

        _parentConnectionNode._connectionLine = null;
        _parentConnectionNode._otherConnectionNode = null;
        
        if(ConnectNodes.instance._prevNodeState == null) _parentConnectionNode.ChangeNodeState(new Disconnected(_parentConnectionNode)); //change to disconnected if no nodes were clicked before
    }
}
