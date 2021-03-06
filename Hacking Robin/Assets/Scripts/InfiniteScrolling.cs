using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InfiniteScrolling : MonoBehaviour
{

    public GameObject[] possibleBackgrounds;
    public GameObject[] possibleObstacles;
    public GameObject[] possibleGoodThings;
    public GameObject player;
    public GameObject currentRoom;
    public GameObject currentObstacle;
    public int maxNumberOfActiveBackgrounds;
    public int maxNumberOfActiveObstacles;
    public Text counterText;
    public int coffeeDuration;
    public int beerDuration;
    public int coffeeReduceFactor;
    private int coinCount;
    public int coin1UPCount;
    private float backgroundWidth;
    private float maxX;
    private int backgroundNumber = 0;
    private Queue<GameObject> backgrounds = new Queue<GameObject>();
    private Queue<GameObject> obstacles = new Queue<GameObject>();
    private CharacterMovement characterMovementScript;
    private int numberActiveCoffee;
    private int numberActiveBeer;
    public GameObject cameraShakeObject;
    private CameraShake cameraShakeScript;
    private SoundManagerScript soundManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        soundManagerScript = player.GetComponent<SoundManagerScript>();
        backgroundWidth = 26f; //TODO: change this value when we change the sprite (hardcoded)
        coinCount = 0;
        numberActiveCoffee = 0;
        numberActiveBeer = 0;
        characterMovementScript = player.GetComponent<CharacterMovement>();
        cameraShakeScript = cameraShakeObject.GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > currentRoom.transform.position.x - 9)
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
            currentObstacle = generateGeneration();
            obstacles.Enqueue(currentObstacle);
            if (obstacles.Count > maxNumberOfActiveObstacles)
            {
                Destroy(obstacles.Dequeue());
            }
        }
    }

    private GameObject generateBackground()
    {
        GameObject room = (GameObject)Instantiate(possibleBackgrounds[Random.Range(0, possibleBackgrounds.Length)]);
        room.SetActive(true);
        room.transform.position = new Vector3(backgroundWidth * (backgroundNumber + 1), currentRoom.transform.position.y, 0);
        return room;
    }

    private GameObject generateGeneration()
    {
        GameObject generation;
        if (Random.Range(0, 100) < 30)
        {
            generation = (GameObject)Instantiate(possibleGoodThings[Random.Range(0, possibleGoodThings.Length)]);
        }
        else
        {
            generation = (GameObject)Instantiate(possibleObstacles[Random.Range(0, possibleObstacles.Length)]);
        }
        generation.SetActive(true);
        switch (generation.tag)
        {
            case "Coins":
                generation.transform.position = new Vector3((backgroundWidth * (backgroundNumber + 1)) - (backgroundWidth / 2), Random.Range(-2f, 2.5f), 0); 
                break;
            case "Coffee":
                generation.transform.position = new Vector3((backgroundWidth * (backgroundNumber + 1)) - (backgroundWidth / 2), Random.Range(-3f, 3f), 0); 
                break;
            case "Beer":
                generation.transform.position = new Vector3((backgroundWidth * (backgroundNumber + 1)) - (backgroundWidth / 2), Random.Range(-3f, 3f), 0); 
                break;
            case "Enemy":
                generation.transform.position = new Vector3((backgroundWidth * (backgroundNumber + 1)) - (backgroundWidth / 2), Random.Range(-3f, 3f), 0); 
                break;  
            default:
                generation.transform.position = new Vector3((backgroundWidth * (backgroundNumber + 1)) - (backgroundWidth / 2), Random.Range(-3f, 3f), 0); 
                break;
        }
        
        return generation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        switch (col.gameObject.tag)
        {
            case "Enemy":
                col.gameObject.SetActive(false);
                soundManagerScript.playSound("destroyingEnemy");
                characterMovementScript.loseHP();
                StartCoroutine(cameraShakeScript.Shake(15f, 30f));
                if (!characterMovementScript.shieldOn && characterMovementScript.lives > 0)
                    StartCoroutine(BlinkPlayer(5, 0.1f));

                break;
            case "ShotEnemy":
                col.gameObject.SetActive(false);
                characterMovementScript.loseHP();
                StartCoroutine(cameraShakeScript.Shake(15f, 30f));
                if (!characterMovementScript.shieldOn && characterMovementScript.lives > 0)
                    StartCoroutine(BlinkPlayer(5, 0.1f));
                break;

            case "Obstacle":
                col.gameObject.SetActive(false);
                soundManagerScript.playSound("destroyingEnemy");
                characterMovementScript.loseHP();
                StartCoroutine(cameraShakeScript.Shake(15f, 30f));
                if (!characterMovementScript.shieldOn && characterMovementScript.lives > 0)
                    StartCoroutine(BlinkPlayer(5, 0.1f));

                break;
            case "Coin":
                col.gameObject.SetActive(false);
                soundManagerScript.playSound("pickCoin");
                setCounterText();

                break;
            case "Coffee":
                col.gameObject.SetActive(false);
                soundManagerScript.playSound("pickPowerUp");
                improveAttackSpeed();
                break;
            case "Beer":
                col.gameObject.SetActive(false);
                soundManagerScript.playSound("pickPowerUp");
                shield();
                break;
            default:
                break;
        }
    }

    void setCounterText()
    {
        coinCount++;
        if (coinCount >= coin1UPCount)
        {
            characterMovementScript.gainHP();
            coinCount -= coin1UPCount;
        }
        counterText.text = " " + coinCount.ToString() + " ";
    }

    void improveAttackSpeed()
    {
        numberActiveCoffee++;
        float previousShotDelay = 1; //TODO: change if shotDelay is changed in charactermovement script
        characterMovementScript.shotDelay = previousShotDelay / coffeeReduceFactor;
        StartCoroutine(coffeeEffect(previousShotDelay));
    }

    IEnumerator coffeeEffect(float previousShotDelay)
    {
        yield return new WaitForSeconds(coffeeDuration);
        numberActiveCoffee--;
        if (numberActiveCoffee == 0)
        {
            characterMovementScript.shotDelay = previousShotDelay;
        }
    }

    void shield()
    {
        numberActiveBeer++;
        characterMovementScript.shieldOn = true;
        characterMovementScript.activateShield();
        StartCoroutine(shieldEffect());
    }

    IEnumerator shieldEffect()
    {
        yield return new WaitForSeconds(beerDuration);
        numberActiveBeer--;
        if (numberActiveBeer == 0)
        {
            characterMovementScript.deactivateShield();
            characterMovementScript.shieldOn = false;
        }
    }   

    public IEnumerator BlinkPlayer(int numBlinks, float seconds)
    {
        SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();

        for (int i = 0; i < numBlinks * 2; i++)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(seconds);
        }
        renderer.enabled = true;
    }

}
