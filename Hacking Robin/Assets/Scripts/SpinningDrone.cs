using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDrone : MonoBehaviour
{
    public float xspeed;                //Floating point variable to store the player's movement speed.
    public float yspeed;
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Use the two store floats to create a new Vector2 variable movement.
        //Vector2 movement = new Vector2 (xspeed, yspeed);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //rb2d.AddForce (movement);
        rb2d.velocity = new Vector2(xspeed * Time.fixedDeltaTime, yspeed * Time.fixedDeltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
         switch (col.gameObject.tag)
        {
            case "Background":
                yspeed = -yspeed;
                break;
            default:
                break;
        }
    }
    
}
