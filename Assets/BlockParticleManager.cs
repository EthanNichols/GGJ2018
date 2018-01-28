using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParticleManager : MonoBehaviour
{
    ParticleSystem system;
    // Use this for initialization
    void Start()
    {
        system = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayParticle()
    {
        system.Play();
    }
}
