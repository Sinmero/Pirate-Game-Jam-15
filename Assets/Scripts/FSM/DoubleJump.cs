using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Jumping
{
    public int _totalJumps = 0;

    private AudioClip _audioClipOnEnter;
    private AudioClip _audioClipOnExit;
    private AudioClip _audioClipAux;

    public DoubleJump (StateMachineHandler stateMachineHandler, AudioClip audioOnEnter = null, AudioClip audioOnExit = null, AudioClip audioAuxClip = null) : base(stateMachineHandler) {
        _playerController._falling.OnLanded += OnLanded;
        
        _audioClipOnEnter = audioOnEnter;
        _audioClipOnExit = audioOnExit;
        _audioClipAux = audioAuxClip;
        
    }



    public override void OnStateEnter()
    {
        
        if(_totalJumps <= 0) return;
        base.OnStateEnter();
        // AudioManager.instance.PlaySoundClip(_audioClipOnEnter);
        _moveVector.y = 0;
        _moveVector.x = _rb.velocity.x;
        _rb.velocity = _moveVector;
        _rb.AddForce(Vector2.up * _playerController._jumpForce, ForceMode2D.Impulse);
        _totalJumps -= 1;
        _playerController._doubleJumpParticles.Play();
    }


    public override void Execute()
    {
        base.Execute();
        
    }



    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _playerController._doubleJumpParticles.Stop();
    }



    public void OnLanded(RaycastHit2D raycastHit2D) {
        // AudioManager.instance.PlaySoundClip(_audioClipAux);
        _totalJumps = 1;
    }
}
