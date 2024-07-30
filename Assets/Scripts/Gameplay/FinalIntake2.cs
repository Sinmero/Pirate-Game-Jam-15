using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FinalIntake2 : FinalIntake
{
    public override void OnIntakeStart()
    {
        base.OnIntakeStart();

        StartCoroutine(ChangeWalls());
    }



    private IEnumerator ChangeWalls() {
        yield return new WaitForSeconds(4);

        WorldChange.instance.ChangeWalls(0.45f, 10);
        yield return new WaitForSeconds(10);
        WorldChange.instance.BleedBricks(0, 10);
    }
}
