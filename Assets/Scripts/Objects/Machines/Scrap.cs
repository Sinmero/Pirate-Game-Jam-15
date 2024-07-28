using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : ScrappableMachine
{
    [SerializeField] private string _resourceType = "0";
    [SerializeField] private int _containedResourceAmount = 0;



    public override void OnInteractHold(Interactor interactor)
    {
        base.OnInteractHold(interactor);

        Inventory.instance[_resourceType] += _containedResourceAmount;

        Destroy(gameObject);
    }



    public override void Init()
    {
        base.Init();
        _interactKey = Controls.keys._interactHold.ToString();
    }
}
