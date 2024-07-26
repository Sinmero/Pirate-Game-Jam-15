using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameSystems : MonoBehaviour
{
    private static GameSystems _gameSystems;
    public static GameSystems instance {get {return _gameSystems;}}
    // public GameSettings _gameSettings {get{if(_gameSettings == null) SystemLogger.instance.Log($"_gameSettings is null at {this}", this); return null;}}
    public delegate void OnSceneChange();
    public event OnSceneChange onSceneChange;
    public bool _isWebGL = false;



    void Awake()
    {
        if(_gameSystems != null && this == _gameSystems) {
            Destroy(this);
        } else {
            _gameSystems = this;
        }
    }



    private IEnumerator Coroutine(Action beforeTimer, float timer, Action afterTimer = null) {
        yield return new WaitForEndOfFrame();
        beforeTimer();
        yield return new WaitForSeconds(timer);
        afterTimer();
    }



    public void CoroutineStart (Action beforeTimer, float timer, Action afterTimer = null) {
        StartCoroutine(Coroutine(beforeTimer, timer, afterTimer));
    }



    public void ChangeScene (string sceneName) {
        SoundLogger.instance.Log($"Changing scenes to {sceneName}", this);
        onSceneChange?.Invoke();
        SceneManager.LoadScene(sceneName);
    }
}
