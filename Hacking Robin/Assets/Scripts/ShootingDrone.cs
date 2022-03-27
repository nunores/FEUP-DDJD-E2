using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDrone : MonoBehaviour
{

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float xspeed;
    public GameObject player;
    public GameObject drone;
    public int timeStopped; // time in seconds it stops in air before leaving
    private bool stop = false;
    private bool stoppedOnce = false; // true if has stopped once
    private float original_xspeed; // copy of original xspeed

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
        original_xspeed = xspeed;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {


	if (drone.transform.position.x - player.transform.position.x < 20 && drone.transform.position.x - player.transform.position.x > 0 && stop == false && stoppedOnce == false){
		xspeed = 10;
		stop = true;
		stoppedOnce = true;
	}
	if(stop == true){
		StartCoroutine(stopWait());
	}
	
		Vector2 newVelocity = rb2d.velocity;
		newVelocity.x = xspeed;
		rb2d.velocity = newVelocity;

    }
    

    IEnumerator stopWait()
    {
		yield return new WaitForSeconds(timeStopped);
		xspeed = original_xspeed;
		stop = false;
    }
    
    
    
}
