using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryMachine : Machine
{
    [SerializeField] private InputNode _node1, _node2, _node3;
    public string _recipe = "";
    private Coroutine _currentCoroutine;



    public override void OnStart()
    {
        base.OnStart();
        _node1.onConnect += NodeConnected;
        _node2.onConnect += NodeConnected;
        _node3.onConnect += NodeConnected;

        _node1.afterDisconnect += NodeDisconnected;
        _node2.afterDisconnect += NodeDisconnected;
        _node3.afterDisconnect += NodeDisconnected;
    }



    public override void NodeConnected()
    {
        base.NodeConnected();
        GetRecipe();
        CheckRecipe();
    }



    public override void NodeDisconnected()
    {
        base.NodeDisconnected();
        GetRecipe();
        CheckRecipe();
    }



    private string GetOtherResouceType(InputNode inputNode)
    {
        ConnectionNode connectionNode = inputNode._otherConnectionNode;
        if (connectionNode == null) return "";
        return connectionNode._machine._resource;
    }



    public void GetRecipe()
    {
        _recipe = GetOtherResouceType(_node1) + GetOtherResouceType(_node2) + GetOtherResouceType(_node3);
        Debug.Log(_recipe);
    }



    public void CheckRecipe()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);

        if (!Items.instance._recipes.ContainsKey(_recipe)) return; //no such recipes exist

        _resource = Items.instance._recipes[_recipe];

        GameplayLogger.instance.Log($"{_resource} recipe was set on {this.name}", this);

        _currentCoroutine = StartCoroutine(ProduceResource());
    }



    public IEnumerator ProduceResource()
    {
        yield return new WaitForSeconds(Items.instance._itemDictionary[_resource]._productionTime);

        if (_node1.SpendRecource(1) && _node2.SpendRecource(1) && _node3.SpendRecource(1))
        {
            _resourceAmount++;
            GameplayLogger.instance.Log($"{_resource} was pruduced. Totalamout is {_resourceAmount}", this);
        }

        _currentCoroutine = StartCoroutine(ProduceResource());
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

        if (_resource != "") Inventory.instance[_resource] += _resourceAmount; //giving the player contained resourses from this machine

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
