using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioId
{

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

    public void InitManager()
    {

    }

    public void PlayAudioClip(AudioId audioId)
    {

    }

    public void PlayMusic()
    {

    }
}
