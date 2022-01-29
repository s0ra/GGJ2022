using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuntime : ActorRuntime
{
    private static PlayerRuntime _instance;
    public static PlayerRuntime Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerRuntime>();
            }
            return _instance;
        }
    }

    private Vector3 _targetPosition;
    private bool _meetTargetPosition;

    public override void Init()
    {
        base.Init();
        _meetTargetPosition = true;
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        if (Input.GetMouseButtonUp(1))
        {
            _targetPosition =
                CameraManager.Instance.MainCamera.
                    ScreenToWorldPoint(Input.mousePosition);
            _meetTargetPosition = false;

            VisualEffectManager.Instance.SpawnVisualEffectAndDestroy(
                new VisualEffectSpawnData(VisualEffectId.MouseWave, _targetPosition
                    , Vector3.one));

            //Flip(!_walkRight);
        }
    }

    private void CheckMeetTarget()
    {
        if (!_meetTargetPosition)
        {
            float distance = Mathf.Abs(
                _targetPosition.x - transform.position.x);
            if (distance <= Time.fixedDeltaTime * _actorData.MoveSpeed)
            {
                _meetTargetPosition = true;
            }
        }
    }

    private void CheckDirection()
    {
        if (!_meetTargetPosition)
        {
            _walkRight = _targetPosition.x > transform.position.x;
        }
    }

    public override void FixedUpdateObject()
    {
        base.FixedUpdateObject();
        /*BoxCollider2D forwardChecker = _walkRight ? _rightWallCheckCollider : _leftWallCheckCollider;
        BoxCollider2D backwardChecker = _walkRight ? _leftWallCheckCollider : _rightWallCheckCollider;
        if (CheckAnyOverlapCollider(forwardChecker))
        {
            //Debug.Log($"forwardChecker:{true}");

            if (!CheckAnyOverlapCollider(backwardChecker))
            {
                //Debug.Log($"backwardChecker:{false}");

                Flip(!_walkRight);
            }
        }*/

        CheckDirection();
        if (!_meetTargetPosition)
        {
            TryWalk();
        }
        else
        {
            StopMoving();
        }
        CheckMeetTarget();
    }
}
