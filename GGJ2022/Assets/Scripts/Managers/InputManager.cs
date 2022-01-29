using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputManager>();
            }
            return _instance;
        }
    }



    private Vector3 screenPoint;
    private Vector3 offset;

    public void InitManager()
    {

    }


    // void OnMouseDown()
    // {
    //     screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

    //     offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    // }

    // void OnMouseDrag()
    // {
    //     Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

    //     Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    //     transform.position = curPosition;

    // }


}



