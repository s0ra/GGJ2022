using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Vector2 velocity;
    public int collided_count = 0;
    protected Rigidbody2D body;
    protected Vector2 last_position;
    protected int last_count = 0;
    protected bool on_ground = false;

    public float snap_up = 0.0f;
    public float snap_down = 0.0f;
    void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 deltaPosition = velocity * Time.fixedDeltaTime;
        velocity += Physics2D.gravity * Time.fixedDeltaTime;

        LayerMask mask = LayerMask.GetMask("ObstaclesWhite");
        RaycastHit2D[] results = new RaycastHit2D[2];
        
        Physics2D.CircleCastNonAlloc(body.position, 0.5f, Vector2.down, results, Mathf.Abs(deltaPosition.y), mask);

        Collider2D c0 = results[0].collider;
        Collider2D c1 = results[1].collider;
        if (c0 && c1)
        {
            deltaPosition.y = 0.0f;
            velocity.y = 0.0f;

            body.position = new Vector2(body.position.x, c1.gameObject.transform.position.y + 0.5f * c1.gameObject.transform.localScale.y + body.transform.localScale.y * 0.5f);
            // bool is_white_0 = rb2d0.gameObject.GetComponent<floor>().is_white;
            // bool is_white_1 = rb2d1.gameObject.GetComponent<floor>().is_white;
            /*
            if (is_white_0 && is_white_1)
            {
                body.position = results[1].rigidbody.position;
                deltaPosition.y = 0.0f;
                velocity.y = 0.0f;
            }
            */
        }
        else
        {
            
        }
        
        results = new RaycastHit2D[2];
        
        mask = LayerMask.GetMask("ObstaclesBlack");
        Physics2D.CircleCastNonAlloc(body.position, 0.5f, Vector2.down, results, Mathf.Abs(deltaPosition.y), mask);

        c0 = results[0].collider;
        c1 = results[1].collider;
        if (c0 && !c1)
        {
            
            deltaPosition.y = 0.0f;
            velocity.y = 0.0f;

            body.position = new Vector2(body.position.x, c0.gameObject.transform.position.y + 0.5f * c0.gameObject.transform.localScale.y + body.transform.localScale.y * 0.5f);
            
            // bool is_white_0 = rb2d0.gameObject.GetComponent<floor>().is_white;
            // bool is_white_1 = rb2d1.gameObject.GetComponent<floor>().is_white;
            /*
            if (is_white_0 && is_white_1)
            {
                body.position = results[1].rigidbody.position;
                deltaPosition.y = 0.0f;
                velocity.y = 0.0f;
            }
            */
        }
        else if (c0 && c1)
        {
            RaycastHit2D[] results_2 = new RaycastHit2D[2];
            Physics2D.CircleCastNonAlloc(body.position + deltaPosition - new Vector2(0.0f, body.transform.localScale.y * 1.0f), 0.5f, Vector2.down, results_2, Mathf.Abs(velocity.y * Time.fixedDeltaTime) , mask);

            if (results_2[0].collider && !results_2[1].collider)
            {
                deltaPosition.y = 0.0f;
                velocity.y = 0.0f;
                body.position = new Vector2(body.position.x, c0.gameObject.transform.position.y - 0.5f * c0.gameObject.transform.localScale.y + body.transform.localScale.y * 0.5f);
            }
            
            // body.position = new Vector2(body.position.x, c1.gameObject.transform.position.y + 0.5f * c1.gameObject.transform.localScale.y + body.transform.localScale.y * 0.5f);
        }
        
    
        body.position = body.position + deltaPosition;
        

        /*
        Vector2 deltaPosition = velocity * Time.fixedDeltaTime;
        
        if (!on_ground)
        {
            if (collided_count == 2)
            {
                deltaPosition *= -1.0f;
                velocity.y += 100.0f * Time.fixedDeltaTime;
                if (velocity.y > -1.0f)
                {
                    velocity = Vector2.zero;
                    on_ground = true;
                }
            }
            else
            {
                velocity += Physics2D.gravity * Time.fixedDeltaTime;
                if (velocity.y < -10.0f)
                {
                    velocity.y = -10.0f;
                }
            }
        }
        else 
        {
            if ((collided_count < 2) && last_position != body.position)
            {
                on_ground = false;
            }
        }
                
        body.position = body.position + deltaPosition;
        
        last_position = body.position;
        last_count = collided_count;
        collided_count = 0;
        */
    }
}