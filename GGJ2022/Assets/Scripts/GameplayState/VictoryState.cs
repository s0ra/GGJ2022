using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : GameplayState
{
    public override GameStateId GameStateId => GameStateId.Victory;

    public override void OnEnter(GameplayStateData gameplayStateData)
    {
        base.OnEnter(gameplayStateData);
        UIManager.Instance.LoadingScreen.ScaleDownTo(
            PlayerRuntime.Instance.transform.position, ChangeGameState);

    }

    private void ChangeGameState()
    {
        int nextLevelId = Mathf.Min(LevelManager.Instance.LevelId + 1
            ,GameConstants.Gameplay.MAX_LEVEL_COUNT);
        GameplayManager.Instance.TryChangeGameState(
            new GameplayStateData()
            {
                LevelId = nextLevelId,
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
