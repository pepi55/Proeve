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

    int noOfActiveParticleSystems = 0;

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

            for (int j = 0; j < noOfActiveParticleSystems; j++)
            {
                ParticleSystemStruct selected = Systems[j];

                selected.setup();

                selected.System.Emit(Random.Range(1, 3));

                ParticleSystem.Particle p;
                int pmcount = selected.System.GetParticles(selected.Particles);

                Vector2 dir;
                dir = obj.direction;

                float angle = Util.Common.VectorToAngle(dir);
                angle -= 180;

                for (int i = 0; i < pmcount; i++)
                {
                    p = selected.Particles[i];
                    if (p.velocity == Vector3.zero)
                    {
                        dir = Util.Common.AngleToVector(angle + Random.Range(-10f, 10f));
                        p.position = (dir * radius) + obj.position;
                        p.startSize = 0.5f;
                        p.startLifetime = 0.5f;
                        p.velocity = dir * Random.Range(0.3f, 2f);
                        p.rotation = Random.Range(0, 360f);
                        selected.Particles[i] = p;
                    }
                }

                selected.System.SetParticles(selected.Particles, pmcount);
            }
        }
    }

    /// <summary>
    /// Init for ParticleSystems
    /// This way it does not enable more than there are particle systems
    /// </summary>
    public void setupParticleSystems()
    {
        noOfActiveParticleSystems = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial.Length > Systems.Length ? Systems.Length : Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial.Length;
        Material[] materials = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ParticleMaterial;

        for (int i = 0; i < noOfActiveParticleSystems; i++)
        {
            Systems[i].System.GetComponent<ParticleSystemRenderer>().material = materials[i];
            Systems[i].setup();
        }
    }

    /// <summary>
    /// Actives click animation when the ball was clicked succesfull
    /// </summary>
    /// <param name="obj">Contains data about the balls current movement</param>
    private void OnBallMoveAnimator(Events.IBallMove obj)
    {
        animator.SetTrigger("click");
    }

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
}
