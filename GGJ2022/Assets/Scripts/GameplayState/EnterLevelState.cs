using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevelState : GameplayState
{
    public override GameStateId GameStateId => GameStateId.EnterLevel;

    public override void OnEnter(GameplayStateData gameplayStateData)
    {
        base.OnEnter(gameplayStateData);
        LevelManager.Instance.SpawnLevel(gameplayStateData.LevelId);
        CameraManager.Instance.SetCameraSize(LevelManager.Instance.CurrentLevelRuntime.CameraSize);
        PixelColliderManager.Instance.FitOrthoSizeToRendererCamera();
        GameplayManager.Instance.TryChangeGameState(new GameplayStateData() { 
            GameStateId = GameStateId.PlayerMove
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
