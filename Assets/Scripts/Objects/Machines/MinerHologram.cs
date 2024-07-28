using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerHologram : MachineHologram
{
    [HideInInspector] public ResourceVein _resourceVein;
    private List<ResourceVein> _resourceVeinList = new List<ResourceVein>();

    public override void OnEnter(Collider2D other)
    {
        base.OnEnter(other);
        ResourceVein resourceVein = other.gameObject.GetComponent<ResourceVein>();
        if (resourceVein != null)
        {
            PhysicsLogger.instance.Log($"{this} is overlapping with {resourceVein.name}", this);
            _resourceVeinList.Add(resourceVein);
            _resourceVein = _resourceVeinList[_resourceVeinList.Count -1];
        }
    }



    public override void OnExit(Collider2D other)
    {
        base.OnExit(other);
        ResourceVein resourceVein = other.gameObject.GetComponent<ResourceVein>();
        if (resourceVein != null)
        {
            PhysicsLogger.instance.Log($"{this} is no longer overlapping with {resourceVein.name}", this);
            _resourceVeinList.Remove(resourceVein);
            if (_resourceVeinList.Count == 0)
            {
                _resourceVein = null;
                return;
            }
            _resourceVein = _resourceVeinList[_resourceVeinList.Count - 1];
        }
    }
}
