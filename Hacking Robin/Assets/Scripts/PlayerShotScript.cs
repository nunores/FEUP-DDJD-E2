using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotScript : MonoBehaviour
{

    public GameObject playerShot;
    public GameObject player;
    private SoundManagerScript soundManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        soundManagerScript = player.GetComponent<SoundManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        Destroy(playerShot);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Enemy":
                col.gameObject.SetActive(false);
                soundManagerScript.playSound("destroyingEnemy");
                Destroy(playerShot);
                break;
            case "Obstacle":
                //col.gameObject.SetActive(false);
                Destroy(playerShot);
                break;
            default:
                break;
        }
    }
}
