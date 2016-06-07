using UnityEngine;

public class GoalAnimationController : MonoBehaviour
{
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
}
