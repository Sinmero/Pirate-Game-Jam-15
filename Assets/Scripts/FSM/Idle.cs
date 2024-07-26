using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Grounded
{
    public Idle(StateMachineHandler stateMachineHandler) :base(stateMachineHandler){}



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        AnimationMaker animationMaker = _playerController._animationMaker;
        animationMaker._minFramesPerSecond = 1;
        animationMaker._maxFramesPerSecond = 1;
        animationMaker._spriteList = _playerController._idleAnimation;
        animationMaker._pingpongAnimation = false;
        animationMaker._loopAnimation = true;
        animationMaker.animateForward();
    }



    public override void Execute()
    {
        base.Execute();
        if(Mathf.Abs(_rb.velocity.x) >= 0.1f) {
            _stateMachineHandler.ChangeState(_playerController._moving);
        }
    }
}
