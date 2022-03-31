using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDrone : MonoBehaviour
{

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float xspeed;
    public float playerSpeed; // Match player speed for drone to appear stopped
    public GameObject player;
    private CharacterMovement characterMovementScript;
    public GameObject drone;
    public int timeStopped; // time in seconds it stops in air before leaving
    public float yspeed; // only activates when stopped
    private bool stop = false;
    private bool stoppedOnce = false; // true if has stopped once
    private float original_xspeed; // copy of original xspeed
    //Shooting
    public GameObject droneShot;
    public float bulletSpeed;
    public float maxReloadTime; // reload time in seconds
    public float minReloadTime; // reload time in seconds
    private Queue<GameObject> currentShots = new Queue<GameObject>();
    private bool canShoot = true;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
        original_xspeed = xspeed;
        characterMovementScript = player.GetComponent<CharacterMovement>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {


	if (drone.transform.position.x - player.transform.position.x < 20 && drone.transform.position.x - player.transform.position.x > 0 && stop == false && stoppedOnce == false){
		stop = true;
		stoppedOnce = true;
		
	}
	if(stop == true){
		StartCoroutine(stopWait());
		Vector2 newVelocityY = rb2d.velocity;
		newVelocityY.y = yspeed;
		rb2d.velocity = newVelocityY;
		if(canShoot == true){
			canShoot = false;
		    	currentShots.Enqueue(droneShooting());
			StartCoroutine(reloadShot());
		}
	}
	
	Vector2 newVelocity = rb2d.velocity;
    if(stop == true) newVelocity.x = characterMovementScript.getHorizontalSpeed();
    else newVelocity.x = -original_xspeed;
	rb2d.velocity = newVelocity;
		
        foreach(GameObject shot in currentShots)
        {
            if (shot != null)
            {
            	if(stop == false && stoppedOnce == true){
            		newVelocity.x = -original_xspeed;
            		}
                Vector2 shotVelocity = new Vector2(-newVelocity.x, 0); //TODO: change this speed, when the player gets faster de bulletspeed doesnt change
                if(shot != null){
                	shot.GetComponent<Rigidbody2D>().velocity = shotVelocity;
                }
                
            }
        }

    }
    
    
    private GameObject droneShooting()
    {
        GameObject shot = (GameObject)Instantiate(droneShot);
        droneShot.SetActive(true);
        shot.transform.position = new Vector3(drone.transform.position.x, drone.transform.position.y, 0);
        return shot;
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
    

    IEnumerator stopWait()
    {
		yield return new WaitForSeconds(timeStopped);
		xspeed = original_xspeed;
		yspeed = 0;
		stop = false;
		Vector2 newVelocityY = rb2d.velocity;
		newVelocityY.y = yspeed;
		rb2d.velocity = newVelocityY;
    }
    
    
    IEnumerator reloadShot()
    {
        float reloadTime = Random.Range(minReloadTime,maxReloadTime);
		yield return new WaitForSeconds(reloadTime);
		canShoot = true;

    }
    
    
    
}
