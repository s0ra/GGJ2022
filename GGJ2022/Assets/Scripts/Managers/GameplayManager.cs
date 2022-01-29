using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager _instance;
    public static GameplayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameplayManager>();
            }
            return _instance;
        }
    }

    public void InitManager()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
