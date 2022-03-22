using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int verticalSpeed;
    public float horizontalSpeed;
    public GameObject player;
    public GameObject playerShot;
    public int bulletSpeed;
    private Queue<GameObject> currentShots = new Queue<GameObject>();
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7, true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            movement = new Vector2(0, verticalSpeed);
        }

        rb2d.AddForce(movement);

        Vector2 newVelocity = rb2d.velocity;
        newVelocity.x = horizontalSpeed;
        rb2d.velocity = newVelocity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentShots.Enqueue(playerShooting());
        }

        Vector2 newVelocity = rb2d.velocity;
        newVelocity.x = horizontalSpeed;
        rb2d.velocity = newVelocity;

        foreach(GameObject shot in currentShots)
        {
            if (shot != null)
            {
                Vector2 shotVelocity = new Vector2(newVelocity.x * bulletSpeed, 0); //TODO: change this speed, when the player gets faster de bulletspeed doesnt change
                shot.GetComponent<Rigidbody2D>().velocity = shotVelocity;
            }
        }
    }

    private GameObject playerShooting()
    {
        GameObject shot = (GameObject)Instantiate(playerShot);
        shot.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        return shot;
    }
}
