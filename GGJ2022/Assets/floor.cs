using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class floor : MonoBehaviour
{
    public bool is_white = false;
    void OnTriggerStay2D(Collider2D other)
    {
        /*
        if (is_white)
        {
            other.gameObject.GetComponent<Player>().collided_count += 1;
        }
        else
        {
            other.gameObject.GetComponent<Player>().collided_count -= 1;
        }
        */
    }
}
