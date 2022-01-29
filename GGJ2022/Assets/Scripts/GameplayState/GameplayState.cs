using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateId
{
    None,
    EnterLevel,
    PlayerMove,
    PauseOnDrag,
    Victory,
    Lose
}

[System.Serializable]
public class GameplayState
{
    public virtual GameStateId GameStateId => GameStateId.None;
    protected GameplayStateData _gameplayStateData;

    public virtual void OnEnter(GameplayStateData gameplayStateData)
    {
        _gameplayStateData = gameplayStateData;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void FixedUpdateState()
    {

    }

    public virtual void OnExit()
    {

    }
}
