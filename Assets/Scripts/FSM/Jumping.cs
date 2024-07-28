using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : InAir
{
    private bool _releasedJump = false;
    private float lowJumpMulti = 4f;

    private AudioClip _audioClipOnEnter;
    private AudioClip _audioClipOnExit;

    public Jumping(StateMachineHandler stateMachineHandler, AudioClip audioOnEnter = null, AudioClip audioOnExit = null) : base(stateMachineHandler) {
        _audioClipOnEnter = audioOnEnter;
        _audioClipOnExit = audioOnExit;
        
    }



    public override void Execute()
    {
        base.Execute();

        if (_rb.velocity.y <= 0)
        { // cant break jump if already falling
            _stateMachineHandler.ChangeState(_playerController._falling);
        }

        if (Input.GetKeyUp(Controls.keys._jump))
        { //if the player releases jump button break the jump
            _releasedJump = true;
        }
        ReleaseJumpEarly();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        AnimationMaker animationMaker = _playerController._animationMaker;
        animationMaker._minFramesPerSecond = 10;
        animationMaker._maxFramesPerSecond = 10;
        animationMaker._spriteList = _playerController._jumpingAnimation;
        animationMaker._pingpongAnimation = false;
        animationMaker._loopAnimation = false;
        animationMaker.animateForward();

        _playerController._handAnimationMaker._spriteList = _playerController._handJumpingAnimation;
        _playerController._handAnimationMaker.animateForward();

        _releasedJump = false;
        // AudioManager.instance.PlaySoundClip(_audioClipOnEnter);
    }


    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _releasedJump = false;
    }



    private void ReleaseJumpEarly()
    {
        if (_releasedJump)
        {
            GameplayLogger.instance.Log("Released jump early", _playerController);
            _moveVector.y = _rb.velocity.y;
            _moveVector += Vector2.up * Physics2D.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
            _moveVector.x = _rb.velocity.x;
            _rb.velocity = _moveVector;
        }
    }
}
