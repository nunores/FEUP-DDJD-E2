using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{

    public float verticalSpeed;
    public float horizontalSpeed;
    public GameObject player;
    public GameObject playerShot;
    public float bulletSpeed;
    public float shotDelay;
    private Queue<GameObject> currentShots = new Queue<GameObject>();
    private Rigidbody2D rb2d;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7, true);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalSpeed, verticalSpeed);

        rb2d.AddForce(movement);

        Vector2 newVelocity = rb2d.velocity;
        newVelocity.x = horizontalSpeed;
        newVelocity.y = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
}
