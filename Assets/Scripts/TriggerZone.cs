using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerZone : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;

    public delegate void OnTrigger();
    public OnTrigger onTriggerEnter;
    public OnTrigger ontriggerLeave;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.isTrigger = true;
    }

    


    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if(playerController == null) return;

        onTriggerEnter?.Invoke();
    }



    private void OnTriggerExit2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if(playerController == null) return;

        onTriggerEnter?.Invoke();
    }
}
