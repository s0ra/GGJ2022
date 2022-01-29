using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnDragState : GameplayState
{
    public override GameStateId GameStateId => GameStateId.PauseOnDrag;

    public override void OnEnter(GameplayStateData gameplayStateData)
    {
        base.OnEnter(gameplayStateData);
        LevelManager.Instance.OnEnterGameState(gameplayStateData.GameStateId);
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void OnExit()
    {
        base.OnExit();

    }
}
