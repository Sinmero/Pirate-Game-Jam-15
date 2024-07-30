using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalIntake : MonoBehaviour
{
    public IntakeMachine _intakeMachine;
    public GameObject _otherItake;



    private void Start() {
        _intakeMachine.onGotDesiredAmount += OnResourceReached;
        _intakeMachine.onStartedGettingResource += OnIntakeStart;
    }



    public virtual void OnResourceReached() {
        if(_otherItake != null) _otherItake.SetActive(true);
        gameObject.SetActive(false);
    }


    public virtual void OnIntakeStart() {}
}
