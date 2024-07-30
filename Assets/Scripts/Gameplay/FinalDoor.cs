using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private IntakeMachine _intakeMachine;



    private void Start() {
        _intakeMachine.onGotDesiredAmount += StopMusic;
    }



    public void StopMusic() {
        AudioManager.instance.Fade(AudioManager.instance._musicAudioSource, 0, 2);
    }
}
