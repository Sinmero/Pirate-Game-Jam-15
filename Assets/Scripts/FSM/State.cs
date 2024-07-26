using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateMachineHandler _stateMachineHandler;

    public State(StateMachineHandler stateMachineHandler) {
        _stateMachineHandler = stateMachineHandler;
    }

    public virtual void OnStateEnter() {}

    public virtual void OnStateLeave() {}

    public virtual void Execute() {}
}
