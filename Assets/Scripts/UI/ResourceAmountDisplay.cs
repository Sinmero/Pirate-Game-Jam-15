using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceAmountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro _resourceAmountDisplayTmpro;
    [SerializeField] private ConnectionNode _connectionNode;
    [SerializeField] private string 
    _prependText = "", 
    __appendText = "";

    private void Awake() {
        _connectionNode.onAmountChange += SetAmountDisplay;
    }



    public void SetAmountDisplay (int amount) {
        _resourceAmountDisplayTmpro.text = _prependText + amount.ToString() + __appendText;
    }
}
