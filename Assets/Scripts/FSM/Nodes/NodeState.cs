using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeState
{
    public ConnectionNode _parentConnectionNode;


    public NodeState(ConnectionNode parentConnectionNode) {
        _parentConnectionNode = parentConnectionNode;
    }



    public virtual void OnStateEnter(){}



    public virtual void OnClick(){}



    public virtual void OnClickOther(NodeState nodeState){}



    public virtual void OnStateExit(){}
}
