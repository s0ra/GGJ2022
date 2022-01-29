using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ActorRuntime : LevelObjectRuntime
{
    [SerializeField] private ActorData _actorData;
    [SerializeField] protected ActorAnimator _actorAnimator;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private LayerMask _wallCheckLayer;

    [SerializeField] protected BoxCollider2D _rightWallCheckCollider;
    [SerializeField] protected BoxCollider2D _leftWallCheckCollider;
    [SerializeField] protected BoxCollider2D _groundCheckCollider;

    [SerializeField] protected bool _walkRight;
    public bool HasKey;

    protected bool _onGround;

    public override void Init()
    {
        base.Init();
        _actorAnimator.Init(this);
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    public override void OnEnterGameplayState(GameStateId gameStateId)
    {
        base.OnEnterGameplayState(gameStateId);
        switch (gameStateId)
        {
            case GameStateId.PlayerMove:
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                break;
            case GameStateId.PauseOnDrag:
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                break;
        }
    }


    public override void UpdateObject()
    {
        base.UpdateObject();
        _actorAnimator.UpdateAnimator();
    }

    public override void FixedUpdateObject()
    {
        base.FixedUpdateObject();
        _onGround = CheckAnyOverlapCollider(_groundCheckCollider);
        //Debug.Log($"_onGround:{_onGround}");
        _groundCheckCollider.gameObject.SetActive(_onGround);
    }

    protected virtual void TryWalk()
    {
        if (_onGround)
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        float xSpeed = (_walkRight?1:-1) * _actorData.MoveSpeed;
        //Debug.Log($"Move xSpeed{xSpeed} MoveSpeed{_actorData.MoveSpeed}");

        float ySpeed = _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);
    }

    protected bool CheckAnyOverlapCollider(BoxCollider2D boxCollider2D)
    {
        Collider2D overlapBox = Physics2D.OverlapBox(
            boxCollider2D.transform.position,
             boxCollider2D.size, 0, _wallCheckLayer);
        return overlapBox != null;
    }

    protected void Flip(bool isRight)
    {
        _walkRight = isRight;
    }

    public override void DestroySelf()
    {
        _actorAnimator.DestroySelf();
        base.DestroySelf();
    }


}
