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
}
