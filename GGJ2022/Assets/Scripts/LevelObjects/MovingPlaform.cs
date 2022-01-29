using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingPlaform : LevelObjectRuntime
{
    
    [SerializeField] float offsetLeft = 0, offsetRight = 0, speed = 1;
    [SerializeField] bool hasReachedRight= false, hasReachedLeft = false;
    Vector3 startPosition = Vector3.zero;
    
    
    void FixedUpdate()
    {
        if (!hasReachedRight)
        {
            if (transform.position.x < startPosition.x + offsetRight)
            {
                // Move platform to the right
                // transform.position
            }
            else if (transform.position.x >= startPosition.x + offsetRight)
            {
                hasReachedRight = true;
                hasReachedLeft = false;
            }
        }
        else if (!hasReachedLeft)
        {
            if (transform.position.x > startPosition.x + offsetLeft)
            {
                // Move platform to the left 
            }
            else if (transform.position.x <= startPosition.x + offsetLeft)
            {
                hasReachedRight = false;
                hasReachedLeft = true;
            }
        }
    }
}
