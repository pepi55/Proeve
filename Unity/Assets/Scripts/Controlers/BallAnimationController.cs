using UnityEngine;
using System.Collections;

public class BallAnimationController : MonoBehaviour
{
    [SerializeField]
    ParticleSystemStruct[] Systems;

    Animator animator;

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

        if (Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial != null)
        {
            setupParticleSystems();
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
        if (Input.GetMouseButtonDown(0))
        {
            ParticleSystemStruct selected = Systems[Random.Range(0, Systems.Length)];

            selected.setup();

            selected.System.Emit(5);

            ParticleSystem.Particle p;
            int pmcount = selected.System.GetParticles(selected.Particles);

            Vector2 dir;
            dir = obj.direction;

            float angle = Util.Common.VectorToAngle(dir);
            angle -= 180;

            for (int i = 0; i < pmcount; i++)
            {
                p = selected. Particles[i];
                if (p.velocity == Vector3.zero)
                {
                    dir = Util.Common.AngleToVector(angle + Random.Range(-5, 5));
                    p.position = (dir * radius) + obj.position;
                    p.startSize = 0.5f;
                    p.startLifetime = 0.5f;
                    p.velocity = dir * Random.Range(0, 2f);
                    selected.Particles[i] = p;
                }
            }

            selected.System.SetParticles(selected.Particles, pmcount);
        }
    }

    public void setupParticleSystems()
    {
        int length = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial.Length < Systems.Length ? Systems.Length : Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial.Length;
        Material[] materials = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial;

        for (int i = 0; i < length; i++)
        {
            Systems[i].System.GetComponent<ParticleSystemRenderer>().material = materials[i];
            Systems[i].setup();
        }
    }

    private void OnBallMoveAnimator(Events.IBallMove obj)
    {
        animator.SetTrigger("click");
    }

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
}
