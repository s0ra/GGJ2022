using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingPlaform : LevelObjectRuntime
{
    [SerializeField] float offsetLeft = 0, offsetRight = 0, speed = 1;
    [SerializeField] bool hasReachedRight = false, hasReachedLeft = false;

    private Vector3 startPosition;

    public override void Init()
    {
        base.Init();
        startPosition = transform.position;
        Debug.Log("init");
    }

    public override void FixedUpdateObject()
    {
        Debug.Log("update");
        base.FixedUpdateObject();

        MoveObject();
    }
    

    void MoveObject()
    {
        if (!hasReachedRight)
        {
            if (transform.position.x < startPosition.x + offsetRight)
            {
                // Move platform to the right
                transform.Translate(Vector2.right * speed * Time.deltaTime);
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
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else if (transform.position.x <= startPosition.x + offsetLeft)
            {
                hasReachedRight = false;
                hasReachedLeft = true;
            }
        }
    }
}