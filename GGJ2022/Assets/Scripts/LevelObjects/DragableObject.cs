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

    [SerializeField] private ParticleSystem draggingParticle;
    private ParticleSystem _currentParticle;

    private bool _dragging;
    private bool _hovering;
    
    public override void Init()
    {
        boxCollider2D.size = spriteRenderer.size;
        outlineSpriteRenderer.size = spriteRenderer.size;
        _dragging = false;
        _hovering = false;
        outlineSpriteRenderer.gameObject.SetActive(false);
        outlineSpriteRenderer.gameObject.GetComponentInChildren<SpriteRenderer>().size =
            spriteRenderer.size;

        // Set up dragging particle
        ParticleSystem.ShapeModule s = draggingParticle.shape;
        s.spriteRenderer = outlineSpriteRenderer;
    }

    private bool CheckPlayerInside()
    {
        if (boxCollider2D.bounds.Contains(PlayerRuntime.Instance.transform.position))
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
        if (boxCollider2D.bounds.Contains(PlayerRuntime.Instance.transform.position))
        {
            return;
        }

        if (!_hovering)
        {
            AudioManager.Instance.PlayAudioClip(AudioId.hover);
        }
        _hovering = true;
        outlineSpriteRenderer.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        _hovering = false;
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

        // Show dragging particle
        _currentParticle = Instantiate(draggingParticle, transform.position, transform.rotation);
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
        _currentParticle.transform.position = curPosition;
        Snapping();
    }

    void OnMouseUp()
    {
        _dragging = false;
        if (_currentParticle != null)
        {
            DestroyImmediate(_currentParticle.gameObject);
        }
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