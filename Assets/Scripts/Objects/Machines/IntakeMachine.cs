using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntakeMachine : Machine
{
    [SerializeField] private int _desiredResourceAmount = 0;
    [SerializeField] private string _desiredResource = "1";
    [SerializeField] private string _desiredResourceState = "S";

    [SerializeField] private SpriteRenderer _icon, _stateIcon;

    public delegate void OnChange();
    public event OnChange onStartedGettingResource;
    public event OnChange onGotDesiredAmount;

    private Coroutine _currentCoroutine;



    public override void OnStart()
    {
        base.OnStart();
        _inputNodeList[0].onConnect += NodeConnected;

        _icon.sprite = Items.instance._itemDictionary[_desiredResource]._icon;
        _stateIcon.sprite = Items.instance._statesDictionary[_desiredResourceState]._stateIcon;
    }



    public override void NodeConnected()
    {
        base.NodeConnected();
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        CheckInputResource();
    }



    public void CheckInputResource()
    {
        ConnectionNode connectionNode = _inputNodeList[0]._otherConnectionNode;
        if (connectionNode == null) return;

        var _resource = connectionNode._machine._resource;
        var _resourceState = connectionNode._machine._resourceState;

        if (_resource + _resourceState != _desiredResource + _desiredResourceState) return;

        onStartedGettingResource?.Invoke();
        _currentCoroutine = StartCoroutine(RecieveResource());
    }



    public IEnumerator RecieveResource()
    {
        yield return new WaitForSeconds(Items.instance._itemDictionary[_desiredResource]._productionTime);

        bool canProduce = true;
        bool loop = true;

        foreach (InputNode item in _inputNodeList)
        {
            if (!item.SpendRecource(1))
            {
                canProduce = false;
                break;
            }
        }

        if (canProduce && _desiredResourceAmount != _resourceAmount)
        {
            _resourceAmount++;
            GameplayLogger.instance.Log($"{_desiredResource} was recieved. Totalamout is {_resourceAmount}", this);
        }

        if (_desiredResourceAmount == _resourceAmount)
        {
            foreach (var item in _inputNodeList)
            {
                item.OnClick();
                Destroy(item.gameObject);
            }
            onGotDesiredAmount?.Invoke();
            loop = false;
        }

        if (loop) _currentCoroutine = StartCoroutine(RecieveResource());
    }
}
