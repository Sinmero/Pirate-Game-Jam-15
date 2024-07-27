using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : Interactable
{
    [SerializeField] private string _resourceType = "0";
    [SerializeField] private int _resourceAmount = 0;



    public override void OnInteractHold(Interactor interactor)
    {
        base.OnInteractHold(interactor);

        Inventory.instance[_resourceType] += _resourceAmount;

        Destroy(gameObject);
    }



    public override void Init()
    {
        base.Init();
        _interactKey = Controls.keys._interactHold.ToString();
    }



    public override string GetPopupMessage () {
        return $"Hold {_interactKey} to {_actionName} {_interactableName}";
    }
}
