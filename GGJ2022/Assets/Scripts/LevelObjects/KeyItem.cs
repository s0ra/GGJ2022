using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : LevelObjectRuntime
{
    private ActorRuntime _player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<ActorRuntime>();
            if (!_player.HasKey)
            {
                _player.HasKey = true;
            }
        }
    }


    public override void UpdateObject()
    {
        base.UpdateObject();
        if (_player.HasKey)
        {
            // fly around player
        }
    }
}