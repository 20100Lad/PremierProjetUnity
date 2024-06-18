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

    Transform _transform;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particlesSystemMainModule = particleSystem.main;

        _transform = transform;

    }

    void LateUpdate()
    {
        int maxParticles = particlesSystemMainModule.maxParticles;

        float maxDistanceSqr = maxDistance * maxDistance;

        int lrIndex = 0;
        int lineRendererCount = lineRenderers.Count;


                if (particles == null || particles.Length < maxParticles)
                {
                    particles = new ParticleSystem.Particle[maxParticles];
                }

                particleSystem.GetParticles(particles);
                int particleCount = particleSystem.particleCount;

        switch (particlesSystemMainModule.simulationSpace)
        {
            case ParticleSystemSimulationSpace.Local:
                {
                    _transform = transform;
                    lineRendererTemplate.useWorldSpace = false;
                    break;
                }
            case ParticleSystemSimulationSpace.Custom:
                {
                    _transform = particlesSystemMainModule.customSimulationSpace;
                    lineRendererTemplate.useWorldSpace = false;
                    break;
                }
            case ParticleSystemSimulationSpace.World:
                {
                    _transform = transform;
                    lineRendererTemplate.useWorldSpace = true;
                    break;
                }
            default:
                {
                    throw new System.NotSupportedException(
                        string.Format("Unsupported simulation space '{0}'.", System.Enum.GetName(typeof(ParticleSystemSimulationSpace), particlesSystemMainModule.simulationSpace)));
                }
        }
                for (int i = 0; i < particleCount; i++)
                {
                    Vector3 p1_position = particles[i].position;

                    for (int j = i + 1; j < particleCount; j++)
                    {
                        Vector3 p2_position = particles[j].position;
                        float distanceSqr = Vector3.SqrMagnitude(p1_position - p2_position);

                        if (distanceSqr <= maxDistanceSqr)
                        {
                            LineRenderer lr;

                            if (lrIndex == lineRendererCount)
                            {
                                lr = Instantiate(lineRendererTemplate, _transform, false);
                                lineRenderers.Add(lr);

                                lineRendererCount++;
                            }
                            lr = lineRenderers[lrIndex];

                            lr.enabled = true;

                            lr.SetPosition(0, p1_position);
                            lr.SetPosition(1, p2_position);

                            lrIndex++;
                        }
                    }
                }

                for (int i = lrIndex; i < lineRendererCount; i++)

                {
                    lineRenderers[i].enabled = false;
                }
        }
}
