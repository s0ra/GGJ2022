using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    private static VisualEffectManager _instance;
    public static VisualEffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VisualEffectManager>();
            }
            return _instance;
        }
    }

    public void InitManager()
    {

    }

    public void SpawnVisualEffect()
    {

    }
}
