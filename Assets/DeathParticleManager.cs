using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleManager : MonoBehaviour
{
    ParticleSystem[] particles;

    // Use this for initialization
    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayParticles()
    {
        foreach (ParticleSystem s in particles)
            s.Play();
    }
}
