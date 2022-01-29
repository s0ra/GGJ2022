using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRuntime : ActorRuntime
{
    public override void FixedUpdateObject()
    {
        base.FixedUpdateObject();

        BoxCollider2D forwardChecker = _walkRight ? _rightWallCheckCollider : _leftWallCheckCollider;
        BoxCollider2D backwardChecker = _walkRight ? _leftWallCheckCollider : _rightWallCheckCollider;
        if (CheckAnyOverlapCollider(forwardChecker))
        {
            //Debug.Log($"forwardChecker:{true}");

            if (!CheckAnyOverlapCollider(backwardChecker))
            {
                //Debug.Log($"backwardChecker:{false}");

                Flip(!_walkRight);
            }
        }

        TryWalk();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameplayManager.Instance.TryChangeGameState(
                new GameplayStateData(GameStateId.Lose));
        }
    }
}
