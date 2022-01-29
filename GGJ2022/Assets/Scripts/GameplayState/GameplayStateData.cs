using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStateData
{
    public GameStateId GameStateId;
    public int LevelId;

    public GameplayState ToGameplayState()
    {
        GameplayState result = null;
        switch (GameStateId)
        {
            case GameStateId.EnterLevel:
                result = new EnterLevelState();
                break;
            case GameStateId.PlayerMove:
                result = new PlayerMoveState();
                break;
            case GameStateId.PauseOnDrag:
                result = new PauseOnDragState();
                break;
            case GameStateId.Victory:
                result = new VictoryState();
                break;
            case GameStateId.Lose:
                result = new LoseState();
                break;
            default:
                result = null;
                break;
        }
        return result;
    }

    public GameplayStateData(GameStateId gameStateId)
    {
        GameStateId = gameStateId;
    }
}
