using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryMachine : ScrappableMachine
{
    public string _recipe = "";
    private Coroutine _currentCoroutine;



    public override void OnStart()
    {
        base.OnStart();

        foreach (InputNode item in _inputNodeList)
        {
            item.onConnect += NodeConnected;
            item.onDisconnect += NodeDisconnected;
        }
    }



    public override void NodeConnected()
    {
        base.NodeConnected();
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        GetRecipe();
        CheckRecipe();
    }



    public override void NodeDisconnected()
    {
        base.NodeDisconnected();
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        GetRecipe();
        CheckRecipe();
    }



    private string GetOtherResouceType(InputNode inputNode)
    {
        ConnectionNode connectionNode = inputNode._otherConnectionNode;
        if (connectionNode == null) return "";
        return connectionNode._machine._resource + connectionNode._machine._resourceState;
    }



    public void GetRecipe()
    {
        string recipe = "";
        foreach (InputNode item in _inputNodeList)
        {
            recipe += GetOtherResouceType(item);
        }

        _recipe = recipe;

        GameplayLogger.instance.Log($"Recipe changed at {this.name} tp {_recipe}", this);
    }



    public void CheckRecipe()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);

        if (!Items.instance._recipes.ContainsKey(_recipe)) return; //no such recipes exist

        var resource = Items.instance._recipes[_recipe];
        _resourceState = Items.instance._itemDictionary[resource]._defaultState;
        _resource = resource;

        GameplayLogger.instance.Log($"{_resource} recipe was set on {this.name}", this);

        _currentCoroutine = StartCoroutine(ProduceResource());
    }



    public IEnumerator ProduceResource()
    {
        yield return new WaitForSeconds(Items.instance._itemDictionary[_resource]._productionTime);

        bool canProduce = true;

        foreach (InputNode item in _inputNodeList)
        {
            if(!item.SpendRecource(1)) {
                canProduce = false;
                break;
            }
        }

        if (canProduce)
        {
            _resourceAmount++;
            GameplayLogger.instance.Log($"{_resource} was pruduced. Totalamout is {_resourceAmount}", this);
        }

        _currentCoroutine = StartCoroutine(ProduceResource());
    }



    // public override void OnInteract(Interactor interactor)
    // {
    //     base.OnInteract(interactor);

    //     Inventory.instance[_resource] += _resourceAmount;
    //     _resourceAmount = 0;
    // }



    // public override void OnInteractSecondary(Interactor interactor)
    // {
    //     base.OnInteractSecondary(interactor);

    //     _resourceAmount += Inventory.instance[_resource]; //putting appropriate resource from inventory to factory
    //     Inventory.instance[_resource] = 0;
    // }
}
