using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShot : MonoBehaviour
{

    public GameObject droneShot;

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
        Destroy(droneShot);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
         switch (col.gameObject.tag)
        {
            case "Player":
                Destroy(droneShot);
                break;
            case "Obstacle":
                //Destroy(droneShot);
                break;
            default:
                break;
        }
    }

}