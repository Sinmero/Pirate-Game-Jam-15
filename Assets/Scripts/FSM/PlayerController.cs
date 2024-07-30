using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : StateMachineHandler
{
    public float _jumpForce, _moveSpeed;
    [HideInInspector] public Falling _falling;
    [HideInInspector] public Idle _idle;
    [HideInInspector] public Moving _moving;
    [HideInInspector] public Jumping _jumping;
    [HideInInspector] public DoubleJump _doubleJump;

    [HideInInspector] public AnimationMaker _animationMaker;
    public AnimationMaker _handAnimationMaker;
    public List<Sprite> _idleAnimation = new List<Sprite>();
    public List<Sprite> _runningAnimation = new List<Sprite>();
    public List<Sprite> _jumpingAnimation = new List<Sprite>();
    public List<Sprite> _fallingAnimation = new List<Sprite>();
    public List<Sprite> _handGroundedAnimation = new List<Sprite>();
    public List<Sprite> _handJumpingAnimation = new List<Sprite>();
    public List<Sprite> _handFallingAnimation = new List<Sprite>();
    public List<AudioClip> _audioClips = new List<AudioClip>();

    public ParticleSystem _doubleJumpParticles;

    public AudioClip _jumpingSound, _landingSound;


    public delegate void OnExecute();
    public event OnExecute onExecute;

    void Start()
    {
        _falling = new Falling(this, _audioClips[3]);
        _idle = new Idle(this);
        _moving = new Moving(this);
        _jumping = new Jumping(this, _audioClips[2]);
        _doubleJump = new DoubleJump(this, _audioClips[2], null, _audioClips[4]);


        _animationMaker = GetComponent<AnimationMaker>();


        ChangeState(_idle);
    }



    public override void OnUpdate()
    {
        base.OnUpdate();
        onExecute?.Invoke();
        MouseClick();
    }



    public void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray, Vector2.zero);
            foreach (RaycastHit2D item in hits)
            {
                var iClickable = item.collider.gameObject.GetComponent<IClickable>();

                if(iClickable != null) {
                    iClickable.IOnClick();
                }
            }
        }
    }
}
