using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleScript : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    private CharacterMovement characterMovementScript;

    public float yToDisappear;
    public GameObject player;
    public float originalParticleVelocity;

    public float timeToGoBack;

    private int currentNumParticles = 0;

    private AudioSource audioSrc;
    private AudioClip paperFlipSound;


    void Start()
    {
        characterMovementScript = player.GetComponent<CharacterMovement>();
        paperFlipSound = Resources.Load<AudioClip>("paperFlip");
        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = 0.3f;
    }
    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_System.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_System.Stop();
        }

        if (m_System.particleCount > currentNumParticles) // Particle is born
        {
            audioSrc.PlayOneShot(paperFlipSound);
        }

        for (int i = 0; i < numParticlesAlive; i++)
        {
            if (m_Particles[i].position.y < yToDisappear)
            {
                m_Particles[i].remainingLifetime = -1.0f;
                // TODO: Add to score 
            }

            float timeAlive = m_Particles[i].startLifetime - m_Particles[i].remainingLifetime;

            m_Particles[i].velocity = new Vector3(characterMovementScript.getHorizontalSpeed(), m_Particles[i].velocity.y, 0);

            if (timeAlive > timeToGoBack)
                m_Particles[i].velocity = new Vector3(originalParticleVelocity, m_Particles[i].velocity.y, 0);


        }

        currentNumParticles = m_System.particleCount;


        // Apply the particle changes to the Particle System
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }
}
