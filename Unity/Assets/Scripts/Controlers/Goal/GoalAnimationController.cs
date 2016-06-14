using UnityEngine;

public class GoalAnimationController : MonoBehaviour
{
    [SerializeField]
    ParticleSystemStruct[] Systems;

    [SerializeField]
    float radius = 0.5f;
    [SerializeField]
    float angle = 25f;
    [SerializeField]
    float arcSize = 45f;

    int noOfActiveParticleSystems = 0;



    public void Start()
    {
        Events.GlobalEvents.AddEventListener<Events.IScore>(OnScoredPoint);

        setupParticleSystems();

    }

    public void OnDestroy()
    {
        Events.GlobalEvents.RemoveEventListener<Events.IScore>(OnScoredPoint);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnScoredPoint(new Events.IScore(0, Vector2.zero));
        }
    }

    public void OnScoredPoint(Events.IScore obj)
    {
        for (int j = 0; j < noOfActiveParticleSystems; j++)
        {
            ParticleSystemStruct selected = Systems[j];

            selected.setup();

            selected.System.Emit(Random.Range(2, 5));

            ParticleSystem.Particle p;
            int pmcount = selected.System.GetParticles(selected.Particles);
            Vector3 dir;
            Vector3 newPos;
            for (int i = 0; i < pmcount; i++)
            {
                p = selected.Particles[i];
                if (p.velocity == Vector3.zero)
                {
                    dir = Util.Common.AngleToVector(angle + Random.Range(arcSize / -2f, arcSize / 2f));
                    newPos = (dir * radius) + transform.position;
                    newPos.z = -3f;
                    p.startSize = 0.5f;
                    p.startLifetime = 0.5f;
                    p.velocity = dir * Random.Range(0.9f, 4f);
                    p.rotation = Random.Range(0, 360f);
                    selected.Particles[i] = p;
                }
            }

            selected.System.SetParticles(selected.Particles, pmcount);
        }
    }

    /// <summary>
    /// Init for ParticleSystems
    /// This way it does not enable more than there are particle systems
    /// </summary>
    public void setupParticleSystems()
    {
        noOfActiveParticleSystems = Systems.Length;
        for (int i = 0; i < noOfActiveParticleSystems; i++)
        {
            Systems[i].setup();
            Systems[i].System.GetComponent<ParticleSystemRenderer>().sortingLayerName = "ForGround";
        }
    }
}
