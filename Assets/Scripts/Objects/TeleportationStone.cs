using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationStone : Interactable
{
    public bool _isActive = false;
    [SerializeField] private TeleportationStone _otherTeleportationStone;
    [SerializeField] private GameObject _rune;



    public override void OnInteract(Interactor interactor)
    {
        base.OnInteract(interactor);
        _isActive = true;
        _rune.SetActive(true);

        if(!_otherTeleportationStone._isActive) return;

        interactor.transform.parent.position = _otherTeleportationStone.transform.position;
        Camera.main.gameObject.transform.position = _otherTeleportationStone.transform.position;
    }
}
