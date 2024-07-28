using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private List<AnimationMaker> _animationMakerList = new List<AnimationMaker>();
    private IntakeMachine _intakeMachine;
    private BoxCollider2D _boxCollider2D;


    void Start()
    {
        _intakeMachine = GetComponent<IntakeMachine>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _intakeMachine.onGotDesiredAmount += OpenDoor;
    }



    public void OpenDoor() {
        foreach (AnimationMaker item in _animationMakerList)
        {
            item.animateForward();
            _boxCollider2D.isTrigger = true;
        }
    }
}
