using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioId
{
    hover,
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    private Dictionary<AudioId, AudioClip> _coreAudioClipCache;

    public void InitManager()
    {
        _coreAudioClipCache = new Dictionary<AudioId, AudioClip>();


        foreach (AudioId audioId in Enum.GetValues(typeof(AudioId)))
        {
            AudioClip audioClip = Resources.Load<AudioClip>(
                GameConstants.ResourcesPath.AudioClipPath + audioId);
            _coreAudioClipCache.Add(audioId, audioClip);

        }
    }

    public void PlayAudioClip(AudioId audioId)
    {
        if (_coreAudioClipCache.ContainsKey(audioId))
        {
            AudioSource.PlayClipAtPoint(_coreAudioClipCache[audioId], CameraManager.Instance.MainCamera.transform.position);
        }
        else
        {
            Debug.LogError("no audio id " + audioId);
        }
    }

    public void PlayMusic()
    {
    }
}