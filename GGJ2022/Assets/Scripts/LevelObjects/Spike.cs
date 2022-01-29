using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : LevelObjectRuntime
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameplayManager.Instance.TryChangeGameState(
                new GameplayStateData(GameStateId.Lose));
        }
    }
}