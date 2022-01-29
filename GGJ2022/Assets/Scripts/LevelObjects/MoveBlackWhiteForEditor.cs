using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class MoveBlackWhiteForEditor : MonoBehaviour
{
    private void Update()
    {
        
#if UNITY_EDITOR

        if (EditorApplication.isPlaying)
        {
            return;
        }
        
        if (Selection.activeObject == null)
        {
            return;
        } 
        if (Selection.activeTransform.gameObject == null)
        {
            return;
        }

        if (Selection.activeTransform.gameObject == gameObject)
        {
            Snapping();
        }
#endif
    }

    private void Snapping()
    {
        Vector2 snappedPos = new Vector2((float)Math.Round(transform.position.x * 2, MidpointRounding.AwayFromZero) / 2,
            (float)Math.Round(transform.position.y * 2, MidpointRounding.AwayFromZero) / 2);
        
        transform.position = snappedPos;
        Vector2 snappedScale = new Vector2((float)Math.Round(transform.localScale.x),
            (float)Math.Round(transform.localScale.y));
        transform.localScale = snappedScale;
    }
}