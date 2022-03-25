using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotScript : MonoBehaviour
{

    public GameObject playerShot;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SetActive(false);
            Destroy(playerShot);
        }
         switch (col.gameObject.tag)
        {
            case "Enemy":
                col.gameObject.SetActive(false);
                break;
            case "Obstacle":
                Destroy(playerShot);
                break;
            default:
                break;
        }
    }
}
