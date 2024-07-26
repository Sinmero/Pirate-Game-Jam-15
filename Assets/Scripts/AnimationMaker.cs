using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimationMaker : MonoBehaviour
{
    public List<Sprite> _spriteList; //list of sprites for animation
    public float _minFramesPerSecond = 1f;
    public float _maxFramesPerSecond = 1f;
    [SerializeField] bool _playOnStart = true;
    public bool _loopAnimation = true;
    public bool _pingpongAnimation = false;
    public bool _randomFrames = false;
    [HideInInspector] public SpriteRenderer _thisSR;
    [HideInInspector] public int _spriteListLength;
    [HideInInspector] public int _currentSpriteIndex = 0; //the index of sprite in spriteList. animation will start using this sprite
    [Range(-1, 1)]
    public int _animationDirection = 1; //set to 1 for forward animation set to -1 for reverse animation set to 0 to stop animation
    private Coroutine _currentCoroutine;
    public delegate void OnFrameChange();
    public OnFrameChange onFrameChange;






    void Awake() {

        _thisSR = gameObject.GetComponent<SpriteRenderer>();

        _spriteListLength = _spriteList.Count;

        if (_playOnStart)
        {
            if (_animationDirection == 1)
            {
                animateForward();
            }
            if (_animationDirection == -1)
            {
                animateBackward();
            }
        }

        if (_randomFrames)
        {
            animateRandom();
        }
    }


    public IEnumerator animate(int index) { //if negative reverse = 1 the animation will go forward if its set to -1 it will go backwards
        int startIndex = index;
        _currentSpriteIndex = Math.Abs(index);

        // thisSR.sprite = spriteList[index];
        for (int i = 0; i < _spriteList.Count; i++)
        {
            onFrameChange?.Invoke(); //frame change delegate

            _thisSR.sprite = _spriteList[_currentSpriteIndex];

            _currentSpriteIndex = Math.Abs(_currentSpriteIndex + _animationDirection*1);

            yield return new WaitForSeconds(1 / UnityEngine.Random.Range(_minFramesPerSecond, _maxFramesPerSecond));
        }



        if (_loopAnimation && !_pingpongAnimation) //loop animation
        {
            _currentCoroutine = StartCoroutine(animate(startIndex));
        } else if (_pingpongAnimation && startIndex > 0) //pingpong loop animation
        {
            startIndex = 0;
            _animationDirection = -_animationDirection;
            _currentCoroutine = StartCoroutine(animate(startIndex));
        } else if (_pingpongAnimation && startIndex == 0)
        {
            startIndex = _spriteListLength - 1;
            _animationDirection = -_animationDirection;
            _currentCoroutine = StartCoroutine(animate(startIndex));
        }

        yield return true; //return true when animation is finished
    }



    public void animateForward() {
        if (_currentCoroutine != null) //stop the current animation to prevent overlaping
        {
            StopCoroutine(_currentCoroutine);
        }
        _spriteListLength = _spriteList.Count; //recount the animation list length in case we changed animations
        _animationDirection = 1;
        _currentCoroutine = StartCoroutine(animate(0));
    }



    public void animateBackward() {
        _spriteListLength = _spriteList.Count;
        _animationDirection = -1;
        _currentCoroutine = StartCoroutine(animate(_spriteListLength - 1));
    }



    public void animateForwardFromFrame(int frameIndex) { //allows to run animation starting from set frame
        _spriteListLength = _spriteList.Count;
        _currentSpriteIndex = frameIndex;
        _animationDirection = 1;
        _currentCoroutine = StartCoroutine(animate(0));
    }



    public void animateBackwardFromFrame(int frameIndex) { //allows to run animation starting from set frame
        _spriteListLength = _spriteList.Count;
        _currentSpriteIndex = frameIndex;
        _animationDirection = -1;
        _currentCoroutine = StartCoroutine(animate(_spriteListLength - 1));
    }



    public void stopAnimation() {
        _spriteListLength = _spriteList.Count;
        _animationDirection = 0;
        _loopAnimation = false;
    }



    public void animateRandom() {
        _spriteListLength = _spriteList.Count;
        onFrameChange += randomFrameInt;
    }



    private void randomFrameInt() {
        int index = (int)UnityEngine.Random.Range(0, _spriteListLength);
        _currentSpriteIndex = index;
    }
}
