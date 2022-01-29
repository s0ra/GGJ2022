using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VisualEffectId
{
    MouseWave = 1,
}
public class VisualEffectSpawnData
{
    public VisualEffectId VisualEffectId;
    public Vector3 Position;
    public Vector3 EulerAngle;
    public Vector3 Scale;
    public Transform SetParent;

    public VisualEffectSpawnData(VisualEffectId visualEffectId, Vector3 position, Vector3 scale)
    {
        VisualEffectId = visualEffectId;
        Position = position;
        Scale = scale;
    }
}
