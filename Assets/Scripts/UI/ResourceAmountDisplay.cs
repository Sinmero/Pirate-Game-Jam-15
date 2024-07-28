using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceAmountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro _resourceAmountDisplayTmpro;
    [SerializeField] private ConnectionNode _connectionNode;
    [SerializeField] private Machine _machine;
    [SerializeField] private string 
    _prependText = "", 
    __appendText = "";

    private void Awake() {
        if(_connectionNode != null) _connectionNode.onAmountChange += SetAmountDisplay;
        if(_machine != null) _machine.onResourceAmountChange += SetAmountDisplay;
    }



    public void SetAmountDisplay (int amount) {
        _resourceAmountDisplayTmpro.text = _prependText + amount.ToString() + __appendText;
    }
}
