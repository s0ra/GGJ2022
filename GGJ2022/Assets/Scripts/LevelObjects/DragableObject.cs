using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DragableObject : MonoBehaviour
{

    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 screenPoint;
    private Vector3 offset;


    private void Start()
    {
        boxCollider2D.size = spriteRenderer.size;
    }


    void OnMouseDown()
    {
        GameplayManager.Instance.TryChangeGameState
            (new GameplayStateData(GameStateId.PauseOnDrag));
        offset = gameObject.transform.position -
         Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        PixelColliderManager.Instance.RegeneratePixelCollider();
        GameplayManager.Instance.TryChangeGameState
            (new GameplayStateData(GameStateId.PlayerMove));
    }
}
