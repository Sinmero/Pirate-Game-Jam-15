using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class OutputNode : ConnectionNode
{
    public override void OnStart()
    {
        base.OnStart();
        _machine.onResourceTypeChange += SetNodeIcon; //change the icon when machine resource type changes
        _machine.onResourceTypeChange += InputIconChange;
        _machine.onResourceTypeChange += SetLineRendererColor;
        onConnect += InputIconChange;
        onConnect += OnClick; //probably bad idea
        onDisconnect += InputIconReset;
        _machine.onResourceAmountChange += AmountChange;
    }



    public void AmountChange (int amount) {
        onAmountChange?.Invoke(amount);
    }



    public override void OnClick()
    {
        base.OnClick();

        SetLineRendererColor();
    }



    public void InputIconChange()
    {
        if(_otherConnectionNode == null) return;
        if (_machine._resource == "") return;

        SystemLogger.instance.Log($"Changing {_otherConnectionNode} icon", this);

        _otherConnectionNode.OnTypeChange?.Invoke(); //telling input node that the output resource changed

        Sprite iconSprite = Items.instance._itemDictionary[_machine._resource]._icon;
        Sprite stateSprite = Items.instance._statesDictionary[_machine._resourceState]._stateIcon;
        _otherConnectionNode._icon.sprite = iconSprite;
        _otherConnectionNode._stateIcon.sprite = stateSprite;

        _otherConnectionNode._circleSprite.color = Items.instance._itemDictionary[_machine._resource]._itemColor;
    }


    public void InputIconReset() {
        if(_otherConnectionNode == null) return;
        SystemLogger.instance.Log($"Resetting {_otherConnectionNode} icon", this);
        _otherConnectionNode._icon.sprite = null;
        _otherConnectionNode._stateIcon.sprite = null;

        _otherConnectionNode._circleSprite.color = Items.instance._itemDictionary["Blue"]._itemColor;
    }



    public void SetLineRendererColor () {
        if (_machine._resource == "" || _connectionLine == null) return;
        _resourceColor = Items.instance._itemDictionary[_machine._resource]._itemColor;

        _connectionLine.startColor = _resourceColor;
        _connectionLine.endColor = _resourceColor;
    }
}
