using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class InputManager : MonoBehaviour
{
    private static GameSceneManager _instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameSceneManager>();
            }
            return _instance;
        }
    }



    private Vector3 screenPoint;
    private Vector3 offset;

    public void InitManager()
    {

    }


    


}



