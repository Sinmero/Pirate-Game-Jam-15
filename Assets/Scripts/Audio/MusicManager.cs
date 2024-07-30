using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip _musicNormal, _musicOhShit;
    [SerializeField] private AudioSource _audioSource;



    private void Start() {
        AudioManager.instance.PlaySoundClip(_musicNormal, _audioSource);
    }
}
