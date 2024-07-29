using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNode : ConnectionNode
{
    public override void OnStart()
    {
        base.OnStart();
        onConnect += SubscribeConnectedResourceAmountChange; //subscibe to ooutputs node machine resourceAmountchange delegate on each each connect
        onDisconnect += UnsubscribeConnectedResourceAmountChange;
    }



    public override void SetNodeIcon()
    {
        if(_otherConnectionNode == null) return;
        if (_otherConnectionNode._machine._resource == "")
        {
            _icon.sprite = null;
            return;
        }
        Sprite iconSprite = Items.instance._itemDictionary[_otherConnectionNode._machine._resource]._icon;
        _icon.sprite = iconSprite; //setting the sprite
    }



    public void SubscribeConnectedResourceAmountChange()
    {
        _otherConnectionNode._machine.onResourceAmountChange += AmountChange;
        onAmountChange?.Invoke(_otherConnectionNode._machine._resourceAmount);
    }



    public void UnsubscribeConnectedResourceAmountChange()
    {
        _otherConnectionNode._machine.onResourceAmountChange -= AmountChange;
        onAmountChange?.Invoke(0);
    }



    public void AmountChange(int amount)
    {
        onAmountChange?.Invoke(amount);
    }



    public bool SpendRecource(int spendAmount)
    {
        if (_otherConnectionNode == null) return true; //it is ok if there is no output connected. this makes recipes with different amount of resources possible
        if (_otherConnectionNode._machine._resourceAmount - spendAmount <= 0) return false; //not enough resources cant produce
        _otherConnectionNode._machine._resourceAmount -= spendAmount;
        return true;
    }



    public bool SpendRecource(int spendAmount, bool singleInput)
    {
        if (_otherConnectionNode == null && singleInput) return false; //it is ok if there is no output connected. this makes recipes with different amount of resources possible
        if (_otherConnectionNode == null) return true;
        if (_otherConnectionNode._machine._resourceAmount - spendAmount <= 0) return false; //not enough resources cant produce
        _otherConnectionNode._machine._resourceAmount -= spendAmount;
        return true;
    }
}
