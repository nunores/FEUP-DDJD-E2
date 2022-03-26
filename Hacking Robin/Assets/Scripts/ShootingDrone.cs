using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDrone : MonoBehaviour
{

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float xspeed;
    public GameObject player;
    public GameObject drone;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {


	if (drone.transform.position.x - player.transform.position.x < 0.1 && drone.transform.position.x - player.transform.position.x > 0){
		xspeed = 10;
		Debug.Log("STOP");
	}
	
		Vector2 newVelocity = rb2d.velocity;
		newVelocity.x = xspeed;
		rb2d.velocity = newVelocity;

    }
    
    
    
}
