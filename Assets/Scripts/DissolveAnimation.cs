using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAnimation : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private Coroutine _currentCoroutine;
    private float _dissolveState = -0.1f;


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }



    public void StartAnimation(float animateTime) {
        _dissolveState = -0.1f;
        _animationState = 0;
        _currentCoroutine = StartCoroutine(Animate(animateTime));
    }



    public void StopAnimation() {
        if(_currentCoroutine == null) return;
        StopCoroutine(_currentCoroutine);
        _dissolveState = -0.1f;
        _animationState = 0;
        _materialPropertyBlock.SetFloat("_Dissolve", _dissolveState);
        _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
    }


    
    private int _animationState = 0;
    private IEnumerator Animate(float animateTime) {
        _dissolveState += animateTime / 20;
        _materialPropertyBlock.SetFloat("_Dissolve", _dissolveState);
        _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
        yield return new WaitForSeconds(animateTime / 20);
        _animationState += 1;
        if(_animationState <= 20) _currentCoroutine = StartCoroutine(Animate(animateTime));
    }
}
