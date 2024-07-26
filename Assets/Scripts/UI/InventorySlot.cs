using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textMesh;

    public void SetInventorySlot(Sprite sprite, int amount) {
        _image.overrideSprite = sprite;
        _textMesh.text = amount.ToString();
    }
}
