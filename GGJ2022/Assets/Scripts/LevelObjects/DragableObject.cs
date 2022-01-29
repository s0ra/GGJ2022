using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DragableObject : LevelObjectRuntime
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer outlineSpriteRenderer;
    private Vector3 screenPoint;
    private Vector3 offset;
    [SerializeField] private bool disableDrag = false;

    


    public override void Init()
    {
        boxCollider2D.size = spriteRenderer.size;
        outlineSpriteRenderer.size = spriteRenderer.size;
    }

    private bool CheckPlayerInside()
    {
        Collider2D overlapBox = Physics2D.OverlapBox(
            boxCollider2D.transform.position,
            boxCollider2D.size, 0, default);
        if (overlapBox != null)
        {
            if (overlapBox.gameObject.CompareTag("Player"))
            {
                Debug.Log("player in cannot drag");
                return true;
            }
        }

        return false;
    }


    void OnMouseDown()
    {
        if (disableDrag)
        {
            Debug.Log("disable drag " + gameObject.name);
            return;
        }
        if (CheckPlayerInside())
        {
            return;
        }
        GameplayManager.Instance.TryChangeGameState
            (new GameplayStateData(GameStateId.PauseOnDrag));
        offset = gameObject.transform.position -
                 Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void OnMouseDrag()
    {
        if (disableDrag)
        {
            return;
        }

        if (CheckPlayerInside())
        {
            return;
        }

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
        Snapping();
    }

    void OnMouseUp()
    {
        if (disableDrag)
        {
            return;
        }

        if (CheckPlayerInside())
        {
            return;
        }

        PixelColliderManager.Instance.RegeneratePixelCollider();
        GameplayManager.Instance.TryChangeGameState
            (new GameplayStateData(GameStateId.PlayerMove));
    }

    private void Snapping()
    {
        Vector2 snappedPos = new Vector2(
            (float) Math.Round(transform.position.x * 2, MidpointRounding.AwayFromZero) / 2,
            (float) Math.Round(transform.position.y * 2, MidpointRounding.AwayFromZero) / 2);

        transform.position = snappedPos;
        Vector2 snappedScale = new Vector2((float) Math.Round(transform.localScale.x),
            (float) Math.Round(transform.localScale.y));
        transform.localScale = snappedScale;
    }
}