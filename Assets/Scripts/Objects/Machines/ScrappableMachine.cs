using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrappableMachine : Machine
{
        public override void OnInteractHold(Interactor interactor)
    {
        base.OnInteractHold(interactor);

        Inventory.instance["9"] += 1; //Returning the machine cost to player

        // Inventory.instance[_resource] += _resourceAmount; //giving the player contained resourses from this machine

        // _resourceVein.gameObject.SetActive(true);

        Destroy(gameObject);
    }



    public override void OnHoldStart(Interactor interactor)
    {
        base.OnHoldStart(interactor);

        _dissolveAnimation.StartAnimation(_interactTime);
    }



    public override void OnHoldRelease()
    {
        base.OnHoldRelease();
        _dissolveAnimation.StopAnimation();
    }
}
