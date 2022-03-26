using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDrone : MonoBehaviour
{
    public float xspeed;                //Floating point variable to store the player's movement speed.
    public float yspeed;
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float degreesPerSec = 360f; 

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(xspeed * Time.fixedDeltaTime, yspeed * Time.fixedDeltaTime);
   
        float rotAmount = degreesPerSec * Time.fixedDeltaTime; 
        float curRot = transform.localRotation.eulerAngles.z; 
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,curRot+rotAmount)); 
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
