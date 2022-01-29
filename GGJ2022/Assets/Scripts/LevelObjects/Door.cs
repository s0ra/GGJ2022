using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : LevelObjectRuntime
{
    private ActorRuntime _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<ActorRuntime>();
            if (_player.HasKey)
            {
                _player.HasKey = false;
                gameObject.SetActive(false);
            }
        }
    }


}