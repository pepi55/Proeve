using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class BallAnimationController : MonoBehaviour
{

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    Animator animator;

    public Material ParticleMaterial { get; set; }

    [SerializeField]
    float radius = 0.5f;

    bool AnimatorActive;
    bool ParticleActive;

    void Start()
    {
        if (Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].AnimationControler)
        {
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].AnimationControler;
            Events.GlobalEvents.AddEventListener<Events.IBallMove>(OnBallMoveAnimator);
            AnimatorActive = true;
        }
        else
        {
            GetComponent<Animator>().enabled = false;
        }

        if (Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial)
        {
            InitializeIfNeeded();
            Events.GlobalEvents.AddEventListener<Events.IBallMove>(OnBallMoveParticle);
            ParticleActive = true;
        }
    }

    public void OnDestroy()
    {
        if (ParticleActive)
        {
            Events.GlobalEvents.RemoveEventListener<Events.IBallMove>(OnBallMoveParticle);
        }

        if (AnimatorActive)
        {
            Events.GlobalEvents.RemoveEventListener<Events.IBallMove>(OnBallMoveAnimator);
        }
    }

    /// <summary>
    /// Trigged when the user has clicked and the ball has done it's actions
    /// This function create the particles that give feedback when the player clicks
    /// </summary>
    /// <param name="obj">Givven parameter that contains the ball direction and the current ball position</param>
    private void OnBallMoveParticle(Events.IBallMove obj)
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
            GetComponent<ParticleSystemRenderer>().material = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial;
        }

        if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
    }

    private void OnBallMoveAnimator(Events.IBallMove obj)
    {
        animator.SetTrigger("click");
    }

}
