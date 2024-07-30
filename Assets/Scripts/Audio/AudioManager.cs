using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _amgr;
    public static AudioManager instance { get { return _amgr; } set { return; } }

    public AudioSource _audioSource;
    public AudioSource _musicAudioSource;
    public MusicManager _musicManager;

    public AudioClip _buzz, _click;



    private void Awake()
    {
        if (_amgr != null)
        {
            Destroy(this);
            return;
        }
        _amgr = this;
    }



    public void PlaySoundClip(AudioClip audioClip)
    {
        SoundLogger.instance.Log(audioClip, this);
        if (audioClip != null)
        {
            AudioSource audioSource = Instantiate(_audioSource);
            audioSource.clip = audioClip;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }



    public void PlaySoundClip(AudioClip audioClip, AudioSource audioSource)
    {
        SoundLogger.instance.Log(audioClip, this);
        if (audioClip != null)
        {
            audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }



    public void PlaySoundClip(AudioClip audioClip, float randomizePitch)
    {
        SoundLogger.instance.Log(audioClip, this);
        if (audioClip != null)
        {
            AudioSource audioSource = Instantiate(_audioSource);

            float pitch = Random.Range(-randomizePitch, randomizePitch) + 1;

            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }



    public void ChangeVolume(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }



    private Coroutine _currentCoroutine;
    public void Fade(AudioSource audioSource, float endVolume, float time)
    {
        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(IFadeOut(audioSource, endVolume, time));
    }



    public IEnumerator IFadeOut(AudioSource audioSource, float endVolume, float time)
    {
        float volume = audioSource.volume;
        float t = 0;

        while (t < 1)
        {
            t += 1 / (time * 20);
            float vol = Mathf.Lerp(volume, endVolume, t);

            audioSource.volume = vol;

            yield return new WaitForSeconds(0.05f);
        }

        if(endVolume == 0) audioSource.Stop();
    }

}
