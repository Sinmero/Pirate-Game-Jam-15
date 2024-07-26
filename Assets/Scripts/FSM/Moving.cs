using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : Grounded
{
    public Moving(StateMachineHandler stateMachineHandler) : base(stateMachineHandler) {}



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        AnimationMaker animationMaker = _playerController._animationMaker;
        animationMaker._minFramesPerSecond = 8;
        animationMaker._maxFramesPerSecond = 8;
        animationMaker._spriteList = _playerController._runningAnimation;
        animationMaker._pingpongAnimation = false;
        animationMaker._loopAnimation = true;
        animationMaker.animateForward();
    }



    public override void Execute()
    {
        base.Execute();
        if(Mathf.Abs(_rb.velocity.x) <= 0.1f) {
            _stateMachineHandler.ChangeState(_playerController._idle);
        }
    }
}
