using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    private AudioClip backgroundMusic;
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = Resources.Load<AudioClip>("backgroundMusic");
        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;
        audioSrc.clip = backgroundMusic;
        audioSrc.volume = 0.2f;
        audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
