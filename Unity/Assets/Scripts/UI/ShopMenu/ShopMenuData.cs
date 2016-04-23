using UnityEngine;
using System.Collections;

public class ShopMenuData : MonoBehaviour {

    public const string ResourceName = "ShopData";

    [SerializeField]
    StoreObject[] characters;

    [SerializeField]
    StoreObject[] backgrounds;


    public StoreObject[] Characters
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
    public struct StoreObject
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
}
