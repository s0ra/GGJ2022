using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : LevelObjectRuntime
{
    private ActorRuntime _player;
    [SerializeField] float offsetUp = 0, offsetDown = 0, floatingSpeed = 1;
    [SerializeField] bool hasReachedUp = false, hasReachedDown = false;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<ActorRuntime>();
            if (!_player.HasKey)
            {
                Debug.Log("player get key");
                _player.HasKey = true;
                _player.KeyItem = this;
            }
        }
    }


    public override void UpdateObject()
    {
        base.UpdateObject();
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (_player == null)
        {
            return;
        }
        if (!_player.gameObject.activeSelf)
        {
            return;
        }
        if (_player.HasKey)
        {
            // fly around player
            Vector3 newPos =  Vector3.Lerp(transform.position, _player.transform.position, Time.deltaTime * 3);
            // if (!hasReachedUp)
            // {
            //     newPos.y += floatingSpeed;
            //     if (newPos.y >= _player.transform.position.y + offsetUp)
            //     {
            //         hasReachedUp = true;
            //         hasReachedDown = false;
            //     }
            // }
            // else if (!hasReachedDown)
            // {
            //     newPos.y -= floatingSpeed;
            //     if (newPos.y <= _player.transform.position.y + offsetDown)
            //     {
            //         hasReachedUp = false;
            //         hasReachedDown = true;
            //     }
            // }

            transform.position = newPos;
        }
    }
}