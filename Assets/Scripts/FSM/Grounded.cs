using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grounded : State
{
    public PlayerController _playerController;
    public CapsuleCollider2D _capsuleCollider2d;
    public Rigidbody2D _rb;
    public Vector2 _moveVector = new Vector2(0, 0),
    _colliderSize = new Vector2(0, 0);
    private LayerMask _layerMask;
    private SpriteRenderer _spriteRenderer;


    public Grounded(StateMachineHandler stateMachineHandler) : base(stateMachineHandler)
    {
        _rb = _stateMachineHandler.gameObject.GetComponent<Rigidbody2D>();
        _playerController = _stateMachineHandler as PlayerController;
        _capsuleCollider2d = _stateMachineHandler.GetComponent<CapsuleCollider2D>();
        _spriteRenderer = stateMachineHandler.GetComponent<SpriteRenderer>();
        _layerMask = LayerMask.GetMask("Solid");
        _colliderSize.x = _capsuleCollider2d.size.x * _stateMachineHandler.transform.localScale.x;
        _colliderSize.y = _capsuleCollider2d.size.y * _stateMachineHandler.transform.localScale.y;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        _playerController._handAnimationMaker._spriteList = _playerController._handGroundedAnimation;
        _playerController._handAnimationMaker.animateForward();
    }



    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _moveVector.x = 0;
    }

    public override void Execute()
    {
        base.Execute();

        if (Input.GetKeyDown(Controls.keys._jump))
        {
            _stateMachineHandler.ChangeState(_playerController._jumping);
            _rb.AddForce(Vector2.up * _playerController._jumpForce, ForceMode2D.Impulse);
            return;
        }
        if (Input.GetKey(Controls.keys._left))
        {
            _moveVector.x = -1 * _playerController._moveSpeed;
            _moveVector.y = _rb.velocity.y;
            _rb.velocity = _moveVector;

            if(_playerController.transform.localScale.x == 1) _playerController.transform.localScale = new Vector3(-1,1,1);
            // if (!_spriteRenderer.flipX) _spriteRenderer.flipX = true;
            
        }
        if (Input.GetKey(Controls.keys._right))
        {
            _moveVector.x = _playerController._moveSpeed;
            _moveVector.y = _rb.velocity.y;
            _rb.velocity = _moveVector;

            if(_playerController.transform.localScale.x == -1) _playerController.transform.localScale = new Vector3(1,1,1);
            // if (_spriteRenderer.flipX) _spriteRenderer.flipX = false;
            
        }
        if (Input.GetKeyUp(Controls.keys._left))
        {
            _moveVector.x = 0;
            _rb.velocity = _moveVector;
        }
        if (Input.GetKeyUp(Controls.keys._right))
        {
            _moveVector.x = 0;
            _rb.velocity = _moveVector;
        }
        BoxCast();
    }



    public void BoxCast()
    {

        RaycastHit2D[] collisions = Physics2D.BoxCastAll(_stateMachineHandler.transform.position, _colliderSize, 0, Vector2.down, 0.1f, _layerMask);

        Debug.DrawRay(_playerController.transform.position - new Vector3(0, _colliderSize.y * 0.5f, 0), new Vector3(0, -0.1f, 0), Color.red);

        // var collisionList = collisions.ToList().FindAll(x => x.collider.name != _stateMachineHandler.name);

        if (collisions.Length == 0)
        { //if we dont collide with solids
            _stateMachineHandler.ChangeState(_playerController._falling);
        }
    }
}
