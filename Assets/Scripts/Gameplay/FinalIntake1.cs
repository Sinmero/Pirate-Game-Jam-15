using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalIntake1 : FinalIntake
{
    [SerializeField] private AnimationMaker _animationMaker;



    public override void OnIntakeStart()
    {
        base.OnIntakeStart();

        StartCoroutine(StartMusic());

        _animationMaker.animateForward();

        WorldChange.instance.DimLight();

        WorldChange.instance.FadeInStorm();
    }



    private IEnumerator StartMusic() {
        yield return new WaitForSeconds(2);

        AudioManager.instance.PlaySoundClip(AudioManager.instance._musicManager._musicOhShit, AudioManager.instance._musicAudioSource);

        AudioManager.instance.Fade(AudioManager.instance._musicAudioSource, 0.45f, 2);
    }
}
