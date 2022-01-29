using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectRuntime : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private VisualEffectSpawnData _visualEffectSpawnData;

    public void Init()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public virtual void OnSpawn(VisualEffectSpawnData visualEffectSpawnData)
    {
        _visualEffectSpawnData = visualEffectSpawnData;

        if (_visualEffectSpawnData.SetParent != null)
        {
            transform.SetParent(_visualEffectSpawnData.SetParent);
            transform.localPosition = _visualEffectSpawnData.Position;
            transform.localEulerAngles = _visualEffectSpawnData.EulerAngle;
            transform.localScale = _visualEffectSpawnData.Scale;
        }
        else
        {
            transform.position = _visualEffectSpawnData.Position;
            transform.eulerAngles = _visualEffectSpawnData.EulerAngle;
            transform.localScale = _visualEffectSpawnData.Scale;
        }
        _particleSystem.Play();
    }

    public virtual void DestroySelf()
    {
        ObjectPoolManager.Instance.DestroyObject(gameObject);
    }

    public void OnParticleSystemStopped()
    {
        Debug.Log($"OnParticleSystemStopped{gameObject.name}");
        DestroySelf();
    }
}
