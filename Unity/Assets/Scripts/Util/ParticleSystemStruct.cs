using UnityEngine;

/// <summary>
/// Struct for particle sytems so i don't have to repeat much code
/// </summary>
[System.Serializable]
public struct ParticleSystemStruct
{
    public bool enabled;
    public ParticleSystem System;
    public ParticleSystem.Particle[] Particles;

    public void setup()
    {
        if (Particles == null || Particles.Length < System.maxParticles)
            Particles = new ParticleSystem.Particle[System.maxParticles];
    }
}
