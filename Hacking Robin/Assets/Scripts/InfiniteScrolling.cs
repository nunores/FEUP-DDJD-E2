using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrolling : MonoBehaviour
{

    public GameObject[] possibleBackgrounds;
    public GameObject[] possibleObstacles;
    public GameObject player;
    public GameObject currentRoom;
    public GameObject currentObstacle;
    public int maxNumberOfActiveBackgrounds;
    public int maxNumberOfActiveObstacles;
    private float backgroundWidth;
    private float maxX;
    private int backgroundNumber = 0;
    private Queue<GameObject> backgrounds = new Queue<GameObject>();
    private Queue<GameObject> obstacles = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        backgroundWidth = 29.56f; //TODO: change this value when we change the sprite (hardcoded)
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > currentRoom.transform.position.x)
        {
            currentRoom = generateBackground();
            backgrounds.Enqueue(currentRoom);
            if (backgrounds.Count > maxNumberOfActiveBackgrounds)
            {
                Destroy(backgrounds.Dequeue());
            }
            backgroundNumber++;
        }
        if (player.transform.position.x > currentObstacle.transform.position.x)
        {
            currentObstacle = generateObstacle();
            obstacles.Enqueue(currentObstacle);
            if (obstacles.Count > maxNumberOfActiveObstacles)
            {
                Destroy(obstacles.Dequeue());
            }
        }
    }

    private GameObject generateBackground(){
        GameObject room = (GameObject)Instantiate(possibleBackgrounds[Random.Range(0, possibleBackgrounds.Length)]);
        room.transform.position = new Vector3(backgroundWidth * (backgroundNumber + 1), 0, 0);
        return room;
    }

    private GameObject generateObstacle(){
        GameObject obstacle = (GameObject)Instantiate(possibleObstacles[Random.Range(0, possibleObstacles.Length)]);
        obstacle.transform.position = new Vector3((backgroundWidth * (backgroundNumber + 1)) - (backgroundWidth / 2), Random.Range(-4.6f, 4.6f), 0); //TODO: hardcoded
        return obstacle;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SetActive(false);
    }
}
