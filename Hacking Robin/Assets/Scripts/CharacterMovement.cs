using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public int verticalSpeed;
    public float horizontalSpeed;
    public float xAccelaration; // add to horizontalSpeed each fixed update
    public float maxXSpeed; // To limit the max speed
    public GameObject player;
    public GameObject playerShot;
    public int bulletSpeed;
    public float shotDelay;
    public bool shieldOn = false;
    public List<GameObject> hearts;
    public GameObject floorCeilling;
    public GameObject shield;
    private Queue<GameObject> currentShots = new Queue<GameObject>();
    private Rigidbody2D rb2d;
    public int lives;
    private bool canShoot = true;
    public GameObject uiManager;
    private Menu_Buttons menu_ButtonsScript;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7, true);
        lives = hearts.Count;
        menu_ButtonsScript = uiManager.GetComponent<Menu_Buttons>();
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
        if(horizontalSpeed<maxXSpeed)
            horizontalSpeed += xAccelaration;
        newVelocity.x = horizontalSpeed;
        rb2d.velocity = newVelocity;
        floorCeilling.GetComponent<Rigidbody2D>().velocity = new Vector2(rb2d.velocity.x, 0);
        shield.GetComponent<Rigidbody2D>().velocity = new Vector2(rb2d.velocity.x, 0);
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<Rigidbody2D>().velocity = new Vector2(rb2d.velocity.x, 0);
        }
        if (player.transform.position.y < -3.5f)
        {
            GetComponent<Animator>().SetBool("isGrounded", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isGrounded", false);
        }
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)){
            menu_ButtonsScript.ShowPausePanel();
        }
        else if (Input.GetKey(KeyCode.Return) && canShoot)
        {
            canShoot = false;
            currentShots.Enqueue(playerShooting());
            StartCoroutine(reload());
        }


        Vector2 newVelocity = rb2d.velocity;

        foreach(GameObject shot in currentShots)
        {
            if (shot != null)
            {
                Vector2 shotVelocity = new Vector2(newVelocity.x * bulletSpeed, 0); //TODO: change this speed, when the player gets faster de bulletspeed doesnt change
                shot.GetComponent<Rigidbody2D>().velocity = shotVelocity;
            }
        }

        shield.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }

    private GameObject playerShooting()
    {
        GameObject shot = (GameObject)Instantiate(playerShot);
        shot.SetActive(true);
        shot.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        return shot;
    }

    public void loseHP()
    {
        if (!shieldOn)
        {
            lives--;
            if (lives <= 0)
            {
                print("Dead");
                menu_ButtonsScript.ShowDeadPanel();
            }

            Destroy(hearts[hearts.Count - 1]);
            hearts.RemoveAt(hearts.Count - 1);

        }
    }

    public void gainHP()
    {
        if(lives <= 5){
            lives++;
            GameObject newHeart = (GameObject)Instantiate(hearts[0]);
            float xPos = hearts[hearts.Count-1].transform.position.x;
            float yPos = hearts[hearts.Count-1].transform.position.y;
            float zPos = hearts[hearts.Count-1].transform.position.z;
            newHeart.transform.position = new Vector3(xPos-1, yPos, zPos);
            hearts.Add(newHeart);
        } else 
            Debug.Log("Max Lives Limit Reached");

        
    }

    public float getHorizontalSpeed(){
        return horizontalSpeed;
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    public void activateShield()
    {
        shield.SetActive(true);
        shield.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }

    public void deactivateShield()
    {
        shield.SetActive(false);
    }
}
