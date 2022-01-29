using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : GameplayState
{
    public override GameStateId GameStateId => GameStateId.Lose;

    public override void OnEnter(GameplayStateData gameplayStateData)
    {
        base.OnEnter(gameplayStateData);
        GameplayManager.Instance.TryChangeGameState(
            new GameplayStateData()
            {
                LevelId = LevelManager.Instance.LevelId,
                GameStateId = GameStateId.EnterLevel
            });
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
