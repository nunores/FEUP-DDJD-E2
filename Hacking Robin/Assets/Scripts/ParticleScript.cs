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

    void Start()
    {
        characterMovementScript = player.GetComponent<CharacterMovement>();
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

        if(Input.GetKeyDown(KeyCode.Space)){
            m_System.Play();
     }
     
        if(Input.GetKeyUp(KeyCode.Space)){
            m_System.Stop();
     }

        for (int i = 0; i < numParticlesAlive; i++)
        {
            if (m_Particles[i].position.y < yToDisappear){
                m_Particles[i].remainingLifetime = -1.0f;
                // TODO: Add to score 
            }
            m_Particles[i].velocity = new Vector3(characterMovementScript.getHorizontalSpeed(), m_Particles[i].velocity.y, 0);
        }

        // Apply the particle changes to the Particle System
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }
}
