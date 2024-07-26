using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MachineHologram : MonoBehaviour
{
    private BoxCollider2D _boxCollider2d;
    [HideInInspector] public List<Machine> _machineList = new List<Machine>();

    void Start()
    {
        _boxCollider2d = GetComponent<BoxCollider2D>();
        // _boxCollider2d.isTrigger = true;
    }



    private void OnTriggerEnter2D(Collider2D other) {
        OnEnter(other);
    }



    private void OnTriggerExit2D(Collider2D other) {
        Machine machine = other.GetComponent<Machine>();
        if(machine != null) {
            _machineList.Remove(machine);
            PhysicsLogger.instance.Log($"{other.name} is no longer overlapping with {this}. Total overlapping {_machineList.Count}", this);
        }
        OnExit(other);
    }


    public virtual void OnEnter(Collider2D other) {
        Machine machine = other.GetComponent<Machine>();
        if(machine != null) {
            _machineList.Add(machine);
            PhysicsLogger.instance.Log($"{other.name} is overlapping with {this}. Total overlapping {_machineList.Count}", this);
        }
    }



    public virtual void OnExit(Collider2D other) {
        
    }
}
