using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items")]
public class ItemSO : ScriptableObject
{
    public string _itemName = "Generic item name";
    public string _itemRecipe = "Generic recipe";
    public float _productionTime = 1f;
    public int _itemIndex = 1;
    public Sprite _icon;

    [ColorUsageAttribute(true,true)]
    public Color _itemColor;
}
