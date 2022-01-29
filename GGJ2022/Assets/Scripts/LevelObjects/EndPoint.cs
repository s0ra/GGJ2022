﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayManager.Instance.TryChangeGameState(new GameplayStateData(GameStateId.Victory));
        }
    }
}