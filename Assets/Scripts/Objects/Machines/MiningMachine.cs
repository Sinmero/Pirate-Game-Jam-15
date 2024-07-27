using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MiningMachine : Machine
{
    private Coroutine _currentCoroutine;
    [HideInInspector] public ResourceVein _resourceVein;


    public override void OnStart()
    {
        base.OnStart();
        _currentCoroutine = StartCoroutine(MineResource());
        _resourceVein.gameObject.SetActive(false); //remove the vein when placed
    }



    private IEnumerator MineResource()
    {
        yield return new WaitForSeconds(Items.instance._itemDictionary[_resource]._productionTime); //wait for respective item production time
        _resourceAmount++;
        _currentCoroutine = StartCoroutine(MineResource());
    }



    public override void OnInteract(Interactor interactor)
    {
        base.OnInteract(interactor);

        Inventory.instance[_resource] += _resourceAmount;
        _resourceAmount = 0;
    }



    public override void OnInteractSecondary(Interactor interactor)
    {
        base.OnInteractSecondary(interactor);

        _resourceAmount += Inventory.instance[_resource]; //putting appropriate resource from inventory to factory
        Inventory.instance[_resource] = 0;
    }



    public override void OnInteractHold(Interactor interactor)
    {
        base.OnInteractHold(interactor);

        Inventory.instance["9"] += 1; //Returning the machine cost to player

        Inventory.instance[_resource] += _resourceAmount; //giving the player contained resourses from this machine

        _resourceVein.gameObject.SetActive(true);

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
