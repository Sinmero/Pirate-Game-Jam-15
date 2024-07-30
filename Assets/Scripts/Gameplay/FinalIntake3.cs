using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalIntake3 : FinalIntake
{
    [SerializeField] private GameObject _opusMatter;
    

    public override void OnIntakeStart()
    {
        base.OnIntakeStart();

        _opusMatter.SetActive(true);
    }
}
