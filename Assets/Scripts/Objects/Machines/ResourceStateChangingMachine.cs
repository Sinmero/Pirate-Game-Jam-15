using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStateChangingMachine : ScrappableMachine
{
    [SerializeField] private string _state; //the state this machine will turn the input resource state into
    private Coroutine _currentCoroutine;

    public override void OnStart()
    {
        base.OnStart();
        _resourceState = _state;

        _inputNodeList[0].onConnect += NodeConnected;
        _inputNodeList[0].onDisconnect += NodeDisconnected;
    }



    public override void NodeConnected()
    {
        base.NodeConnected();
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        SetProductionType();
    }



    public override void NodeDisconnected()
    {
        base.NodeDisconnected();
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        SetProductionType();
    }



    public void SetProductionType() { //
        ConnectionNode connectionNode = _inputNodeList[0]._otherConnectionNode;
        if (connectionNode == null) {
            _resource = "";
            return;
        }
        _resource = connectionNode._machine._resource;
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
}
