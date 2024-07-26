using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _inst;
    public static Inventory instance {get{return _inst;} set{_inst = value;}}
    private Dictionary<string, int> _inv = new Dictionary<string, int>();
    public int this[string key] //use this[key] to access inventory
    {
        get { if(_inv.ContainsKey(key)) return _inv[key]; return -1; }

        set { if(_inv.ContainsKey(key)) {_inv[key] = value; onAmountChange?.Invoke(key, value); return;} _inv.Add(key, value); onAmountChange?.Invoke(key, value);}
    }

    public delegate void OnAmountChange(string key, int value);
    public OnAmountChange onAmountChange;



    void Awake()
    {
        if(_inst != null) {
            Destroy(this);
            return;
        }
        _inst = this;

        foreach (KeyValuePair<string, ItemSO> item in Items.instance._itemDictionary) //add all items into inventory and set the amount to 0
        {
            this[item.Value._itemName] = 0;
        }
    }
}
