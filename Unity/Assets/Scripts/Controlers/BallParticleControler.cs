using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class BallParticleControler : MonoBehaviour
{

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;


    public Material ParticleMaterial { get; set; }

    [SerializeField]
    float radius = 0.5f;

    void Start()
    {
        Events.GlobalEvents.AddEventListener<Events.IBallMove>(OnBallMove);
    }

    public void OnDestroy()
    {
        Events.GlobalEvents.RemoveEventListener<Events.IBallMove>(OnBallMove);
    }

    private void OnBallMove(Events.IBallMove obj)
    {
        InitializeIfNeeded();

        if (Input.GetMouseButtonDown(0))
        {
            m_System.Emit(5);

            InitializeIfNeeded();

            ParticleSystem.Particle p;
            int pmcount = m_System.GetParticles(m_Particles);

            Vector2 dir;
            dir = obj.direction;

            float angle = Util.Common.VectorToAngle(dir);
            angle -= 180;

            for (int i = 0; i < pmcount; i++)
            {
                p = m_Particles[i];
                if (p.velocity == Vector3.zero)
                {
                    dir = Util.Common.AngleToVector(angle + Random.Range(-5, 5));
                    p.position = (dir * radius) + obj.position;
                    p.startSize = 0.5f;
                    p.startLifetime = 0.5f;
                    p.velocity = dir * Random.Range(0, 2f);
                    m_Particles[i] = p;
                }
            }

            m_System.SetParticles(m_Particles, pmcount);
        }
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
        {
            m_System = GetComponent<ParticleSystem>();
            GetComponent<ParticleSystemRenderer>().material = ParticleMaterial;
        }

        if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];


    }
}
