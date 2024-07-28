using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private static Items items;
    public static Items instance {get{return items;} set{return;}}

    [SerializeField] private List<ItemSO> _itemsList = new List<ItemSO>();
    [SerializeField] private List<StateSO> _stateList = new List<StateSO>();
    public Dictionary<string, ItemSO> _itemDictionary = new Dictionary<string, ItemSO>(); //get acces to itemSo from here
    public Dictionary<string, string> _recipes = new Dictionary<string, string>(); //get item from a recipe here
    public Dictionary<string, StateSO> _statesDictionary = new Dictionary<string, StateSO>();



    private void Awake() {
        if(items != null) Destroy(this);
        items = this;

        foreach (ItemSO item in _itemsList)
        {
            _itemDictionary.Add(item._itemName, item);

            _recipes.Add(item._itemRecipe, item._itemName); //initializing recipes
        }



        foreach (StateSO item in _stateList)
        {
            _statesDictionary.Add(item._stateName, item);
        }
    }
}
