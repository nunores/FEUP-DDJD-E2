using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    private AudioClip playerGunSound;
    private AudioClip pickCoinSound;
    private AudioClip pickPowerUpSound;
    private AudioClip gettingHitSound;
    private AudioClip destroyingEnemySound;
    private AudioClip enemyShotSound;
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerGunSound = Resources.Load<AudioClip>("playerGunSound");
        pickCoinSound = Resources.Load<AudioClip>("pickCoin");
        pickPowerUpSound = Resources.Load<AudioClip>("pickPowerUp");
        gettingHitSound = Resources.Load<AudioClip>("gettingHit");
        destroyingEnemySound = Resources.Load<AudioClip>("destroyingEnemy");
        enemyShotSound = Resources.Load<AudioClip>("enemyShot");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(string sound)
    {
        switch(sound)
        {
            case "playerGun":
                audioSrc.PlayOneShot(playerGunSound);
                break;
            case "pickCoin":
                audioSrc.PlayOneShot(pickCoinSound);
                break;
            case "pickPowerUp":
                audioSrc.PlayOneShot(pickPowerUpSound);
                break;
            case "gettingHit":
                audioSrc.PlayOneShot(gettingHitSound);
                break;
            case "destroyingEnemy":
                audioSrc.PlayOneShot(destroyingEnemySound);
                break;
            case "enemyShot":
                audioSrc.PlayOneShot(enemyShotSound);
                break;
            default:
                break;
        }
    }
}
