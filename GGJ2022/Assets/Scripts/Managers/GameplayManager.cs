using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager _instance;
    public static GameplayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameplayManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private GameplayState _currentGameplayState;

    [SerializeField] private GameStateId _gameStateId;
    public GameStateId GameStateId => _gameStateId;

    public void InitManager()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_currentGameplayState != null)
        {
            _currentGameplayState.UpdateState();
        }
    }

    private void FixedUpdate()
    {
        if (_currentGameplayState != null)
        {
            _currentGameplayState.FixedUpdateState();
        }
    }

    public void TryChangeGameState(GameplayStateData gameplayStateData)
    {
        Debug.Log($"TryChangeGameState {gameplayStateData.GameStateId}");

        if (_currentGameplayState != null)
        {
            _currentGameplayState.OnExit();
        }
        _currentGameplayState = gameplayStateData.ToGameplayState();
        if (_currentGameplayState != null)
        {
            _gameStateId = _currentGameplayState.GameStateId;
            _currentGameplayState.OnEnter(gameplayStateData);
            Debug.Log($"_currentGameplayState OnEnter {gameplayStateData.GameStateId}");
        }
        else
        {
            _gameStateId = GameStateId.None;
        }
    }




}
