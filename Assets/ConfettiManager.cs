using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : MonoBehaviour
{
    private ParticleSystem particles;

    private static ConfettiManager instance;
    public static ConfettiManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
        particles = GetComponent<ParticleSystem>();
    }

    public void StartConfetti()
    {
        particles.Play();
    }

    public void StopConfetti()
    {
        particles.Stop();
    }
}
