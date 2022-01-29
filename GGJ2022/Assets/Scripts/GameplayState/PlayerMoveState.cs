using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : GameplayState
{
    public override GameStateId GameStateId => GameStateId.PlayerMove;

    public override void OnEnter(GameplayStateData gameplayStateData)
    {
        base.OnEnter(gameplayStateData);
        LevelManager.Instance.OnEnterGameState(gameplayStateData.GameStateId);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        LevelManager.Instance.UpdateManager();
        UIManager.Instance.SetRetryButtonActive(true);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        LevelManager.Instance.FixedUpdateManager();

    }

    public override void OnExit()
    {
        base.OnExit();
        UIManager.Instance.SetRetryButtonActive(false);
    }
}
