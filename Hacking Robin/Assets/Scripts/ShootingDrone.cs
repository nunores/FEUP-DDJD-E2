using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDrone : MonoBehaviour
{

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float xspeed;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        Vector2 newVelocity = rb2d.velocity;
        newVelocity.x = xspeed;
        rb2d.velocity = newVelocity;

    }
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
         switch (col.gameObject.tag)
        {
            case "Background":
                xspeed = 10;
                Debug.Log("Speed = 0");
                break;
            default:
                break;
        }
    }
    
    
}
