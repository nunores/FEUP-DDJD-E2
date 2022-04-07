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
    private AudioSource[] audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerGunSound = Resources.Load<AudioClip>("playerGunSound");
        pickCoinSound = Resources.Load<AudioClip>("pickCoin");
        pickPowerUpSound = Resources.Load<AudioClip>("pickPowerUp");
        gettingHitSound = Resources.Load<AudioClip>("gettingHit");
        destroyingEnemySound = Resources.Load<AudioClip>("destroyingEnemy");
        enemyShotSound = Resources.Load<AudioClip>("enemyShot");

        audioSrc = GetComponents<AudioSource>();
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
                audioSrc[0].volume = 1f;
                audioSrc[0].PlayOneShot(playerGunSound);
                break;
            case "pickCoin":
                audioSrc[1].volume = 0.2f;
                audioSrc[1].PlayOneShot(pickCoinSound);                
                break;
            case "pickPowerUp":
                audioSrc[1].volume = 0.5f;
                audioSrc[1].PlayOneShot(pickPowerUpSound);
                break;
            case "gettingHit":
                audioSrc[1].volume = 1f;
                audioSrc[1].PlayOneShot(gettingHitSound);
                break;
            case "destroyingEnemy":
                audioSrc[1].volume = 0.7f;
                audioSrc[1].PlayOneShot(destroyingEnemySound);
                break;
            case "enemyShot":
                audioSrc[1].volume = 0.5f;
                audioSrc[1].PlayOneShot(enemyShotSound);
                break;
            default:
                break;
        }
    }
}
