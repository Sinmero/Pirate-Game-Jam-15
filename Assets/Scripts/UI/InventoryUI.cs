using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySlotGO;
    [SerializeField] private GameObject _inventoryGO;
    [SerializeField] private GameObject _knowledgeTab;
    private Dictionary<string, InventorySlot> _inventorySlots = new Dictionary<string, InventorySlot>();



    private void Start()
    {
        Inventory.instance.onAmountChange += OnInventoryChange;
    }



    private void Update()
    {
        if (Input.GetKeyDown(Controls.keys._inventory))
        {
            _inventoryGO.SetActive(!_inventoryGO.activeSelf);
        }

        if(Input.GetKeyDown(Controls.keys._tab)) { //this probably shouldnt be here
            _knowledgeTab.SetActive(!_knowledgeTab.activeSelf);
        }
    }



    public void OnInventoryChange(string key, int value)
    {

        GameplayLogger.instance.Log($"{key} amount in inventory changed to {value}", this);

        if (Inventory.instance[key] == 0)
        {
            if (!_inventorySlots.ContainsKey(key)) return;

            Destroy(_inventorySlots[key].gameObject); // destroy inventory ui slot if it has 0 items in it

            _inventorySlots.Remove(key);
            
            return;
        };

        InventorySlot inventorySlot;

        if (_inventorySlots.ContainsKey(key)) //if the required slot already exists
        {
            _inventorySlots[key].SetInventorySlot(Items.instance._itemDictionary[key]._icon, value);
            return;
        }

        GameObject inventorySlotGO = Instantiate(_inventorySlotGO, _inventoryGO.transform.position, quaternion.identity, _inventoryGO.transform);
        inventorySlot = inventorySlotGO.GetComponent<InventorySlot>();
        _inventorySlots.Add(key, inventorySlot);

        inventorySlot.SetInventorySlot(Items.instance._itemDictionary[key]._icon, value);
    }
}
