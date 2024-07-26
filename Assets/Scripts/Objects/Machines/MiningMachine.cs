using System.Collections;
using System.Collections.Generic;
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


    private void Destroy()
    {
        _resourceVein.gameObject.SetActive(true); //return the vein when dissassembled
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
}
