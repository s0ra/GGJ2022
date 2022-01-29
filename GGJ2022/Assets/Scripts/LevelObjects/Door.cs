using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : LevelObjectRuntime
{
    private ActorRuntime _player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player get door");
            _player = other.gameObject.GetComponent<ActorRuntime>();
            if (_player.HasKey)
            {
                Debug.Log("player has key");
                _player.HasKey = false;
                _player.KeyItem.gameObject.SetActive(false);
                _player.KeyItem = null;
                gameObject.SetActive(false);
            }
        }
    }


}