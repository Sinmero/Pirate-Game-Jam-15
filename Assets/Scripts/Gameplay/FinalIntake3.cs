using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalIntake3 : FinalIntake
{
    [SerializeField] private GameObject _opusMatter;
    [SerializeField] private Material _fadeScreen;
    

    public override void OnIntakeStart()
    {
        base.OnIntakeStart();

        _opusMatter.SetActive(true);
    }



    public override void OnResourceReached()
    {
        StartCoroutine(Ending());
    }



    private IEnumerator Ending() {
        GameSystems.instance.ShaderTransition(_fadeScreen, "_Fade", 1, 2);
        AudioManager.instance.Fade(AudioManager.instance._musicAudioSource, 0, 2);

        yield return new WaitForSeconds(5);
        GameSystems.instance.ChangeScene("Outro");
    }
}
