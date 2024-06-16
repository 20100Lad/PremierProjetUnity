using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSystem))]
public class Plexus : MonoBehaviour
{
    public float maxDistance = 1.0f;

    new ParticleSystem particleSystem; 
    ParticleSystem.Particle[] particles;

    ParticleSystem.MainModule particlesSystemMainModule;

    public LineRenderer lineRendererTemplate;
    List<LineRenderer> lineRenderers = new List<LineRenderer>();
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particlesSystemMainModule = particleSystem.main;
    }

    void LateUpdate()
    {
        int maxParticles = particlesSystemMainModule.maxParticles;

        float maxDistanceSqr = maxDistance * maxDistance;

        if (particles == null || particles.Length <maxParticles)
        {
            particles = new ParticleSystem.Particle[maxParticles];
        }

        particleSystem.GetParticles(particles);
        int particleCount = particleSystem.particleCount;

        for (int i = 0; i < particleCount; i++) 
        {
            Vector3 p1_position = particles[i].position;

            for (int j = i + 1; j < particleCount; j++) 
            {
                Vector3 p2_position = particles[i].position;
                float distanceSqr = Vector3.SqrMagnitude(p1_position - p2_position);

            }
        }


    }
}
