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


    private bool _dragging;
    
    public override void Init()
    {
        boxCollider2D.size = spriteRenderer.size;
        outlineSpriteRenderer.size = spriteRenderer.size;
        _dragging = false;
        outlineSpriteRenderer.gameObject.SetActive(false);
        outlineSpriteRenderer.gameObject.GetComponentInChildren<SpriteRenderer>().size =
            spriteRenderer.size;
    }

    private bool CheckPlayerInside()
    {
        if (boxCollider2D.bounds.Contains(ActorRuntime.Instance.transform.position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void OnMouseOver()
    {
        if (boxCollider2D.bounds.Contains(ActorRuntime.Instance.transform.position))
        {
            return;
        }
        outlineSpriteRenderer.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        outlineSpriteRenderer.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (disableDrag)
        {
            Debug.Log("disable drag " + gameObject.name);
            return;
        }

        Debug.Log("player inside " + CheckPlayerInside());
        if (CheckPlayerInside())
        {
            return;
        }

        _dragging = true;
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
        if (!_dragging && CheckPlayerInside())
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
        _dragging = false;
        if (disableDrag)
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