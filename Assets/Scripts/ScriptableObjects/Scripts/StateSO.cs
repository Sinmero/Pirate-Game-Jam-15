using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/States")]
public class StateSO : ScriptableObject
{
    public string _stateName = "Generic state";
    public Sprite _stateIcon;
}
