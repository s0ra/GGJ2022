using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : LevelObjectRuntime
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayManager.Instance.TryChangeGameState(
                new GameplayStateData(GameStateId.Lose));
        }
    }
}