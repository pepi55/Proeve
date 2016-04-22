using UnityEngine;
using System.Collections;

public class ShopMenuData : MonoBehaviour {

    public const string ResourceName = "ShopData";

    [SerializeField]
    Sprite[] characters;
    [SerializeField]
    Sprite[] characterPreviewImages;
    [SerializeField]
    Sprite[] backgrounds;

    public Sprite[] Characters
    {
        get
        {
            return characters;
        }
    }

    public Sprite[] Backgrounds
    {
        get
        {
            return backgrounds;
        }
    }

    public Sprite[] CharacterPreviewImages
    {
        get
        {
            return characterPreviewImages;
        }
    }
}
