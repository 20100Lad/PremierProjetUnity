using UnityEngine;

public class ParticuleSeek : MonoBehaviour
{
    public Transform target;
    public float force = 10.0f;

    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];

        ps.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystem.Particle p = particles[i];

            Vector3 particlesWorldPosition;

            if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Local)
            {
                particlesWorldPosition = transform.TransformPoint(p.position);    
            }
            else if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Custom)
            {
                particlesWorldPosition = ps.main.customSimulationSpace.TransformPoint(p.position);
            }
            else 
            {
                particlesWorldPosition = p.position;
            }

            Vector3 directionToTarget = (target.position - particlesWorldPosition).normalized;
            Vector3 seekForce = (directionToTarget * force) * Time.deltaTime;

            p.velocity += seekForce;

            particles[i] = p;
        }
        ps.SetParticles(particles, particles.Length);

    }
}
