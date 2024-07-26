using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerHologram : MachineHologram
{
    [HideInInspector] public ResourceVein _resourceVein;


    public override void OnEnter(Collider2D other)
    {
        base.OnEnter(other);
        ResourceVein resourceVein = other.gameObject.GetComponent<ResourceVein>();
        if(resourceVein != null) {
            PhysicsLogger.instance.Log($"{this} is overlapping with {resourceVein.name}", this);
            _resourceVein = resourceVein;
        }
    }



    public override void OnExit(Collider2D other)
    {
        base.OnExit(other);
        ResourceVein resourceVein = other.gameObject.GetComponent<ResourceVein>();
        if(resourceVein != null) {
            PhysicsLogger.instance.Log($"{this} is no longer overlapping with {resourceVein.name}", this);
            _resourceVein = null;
        }
    }
}
