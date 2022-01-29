using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VisualEffectId
{

}
public class VisualEffectSpawnData
{
    public VisualEffectId VisualEffectId;
    public Vector3 Position;
    public Vector3 EulerAngle;
    public Vector3 Scale;
    public Transform SetParent;
}
