using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace Menus
{
    /// <summary>
    /// Data class that contains all the data of the ball and backgrounds so it's easily accesable
    /// </summary>
    public class ShopMenuData : MonoBehaviour
    {
        /// <summary>
        /// Gets the ShopMenuData and loads it from disk if needed
        /// </summary>
        /// <returns>an instance of the ShopMenuData</returns>
        public static ShopMenuData GetShopMenu()
        {
            ShopMenuData data;
            data = FindObjectOfType<Menus.ShopMenuData>();
            if (!data)
            {
                GameObject gameobj = Instantiate(Resources.Load(ResourceName)) as GameObject;

                data = gameobj.GetComponent<Menus.ShopMenuData>();
            }

            return data;
        }

        /// <summary>
        /// name of the the shopmenu in the resource folder
        /// </summary>
        public const string ResourceName = "ShopData";

        /// <summary>
        /// Array that contains all the data needed for the characters/balls/bounceobjects
        /// </summary>
        [SerializeField]
        BallStoreObject[] characters;

        /// <summary>
        /// Array that contains all the data need for the backgrounds
        /// </summary>
        [SerializeField]
        StoreObject[] backgrounds;


        public BallStoreObject[] Characters
        {
            get
            {
                return characters;
            }
        }

        public StoreObject[] Backgrounds
        {
            get
            {
                return backgrounds;
            }
        }

        [System.Serializable]
        public class StoreObject
        {
            public int Cost;
            public string Name;

            [SerializeField]
            Sprite highRes;
            public Sprite HighRes
            {
                get
                {
                    if (highRes)
                    {
                        return highRes;
                    }
                    return Sprite.Create(new Texture2D(512, 512), new Rect(0, 0, 512, 512), Vector2.one / 2f);
                }
            }
            [SerializeField]
            Sprite lowRes;
            public Sprite LowRes
            {
                get
                {
                    if (lowRes)
                    {
                        return lowRes;
                    }
                    return Sprite.Create(new Texture2D(512, 512), new Rect(0, 0, 512, 512), Vector2.one / 2f);
                }
            }
        }

        [System.Serializable]
        public class BallStoreObject : StoreObject
        {
            [SerializeField]
            Material[] particleMaterial;
            public Material[] ParticleMaterial
            {
                get
                {
                    if (particleMaterial != null)
                    {
                        return particleMaterial;
                    }
                    // return (Material)Resources.Load("DefaultParticleMaterial");
                    return null;
                }
            }

            [SerializeField]
            RuntimeAnimatorController animationControler;
            public RuntimeAnimatorController AnimationControler
            {
                get
                {
                    if (animationControler)
                    {
                        return animationControler;
                    }
                    return null;
                }
            }

            [SerializeField]
            AudioClip hitSound;
            public AudioClip HitSound
            {
                get
                {
                    if (hitSound)
                    {
                        return hitSound;
                    }
                    return null;
                }
            }

            [SerializeField]
            AudioClip scoreSound;
            public AudioClip ScoreSound
            {
                get
                {
                    if (scoreSound)
                    {
                        return scoreSound;
                    }
                    return null;
                }
            }
        }
    }
}