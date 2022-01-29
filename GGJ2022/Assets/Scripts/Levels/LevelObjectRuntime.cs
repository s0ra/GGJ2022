using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectRuntime : MonoBehaviour
{
    public virtual void Init()
    {
        //Debug.Log($"{gameObject.name} LevelObjectRuntime Init");
    }

    public virtual void OnEnterGameplayState(GameStateId gameStateId)
    {

    }

    public virtual void UpdateObject()
    {

    }

    public virtual void FixedUpdateObject()
    {

    }

    public virtual void DestroySelf()
    {

    }
}
