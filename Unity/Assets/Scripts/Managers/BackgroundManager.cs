using UnityEngine;
using System.Collections;
using Util;
public class BackgroundManager : MonoBehaviour {

    [SerializeField]
    SpriteRenderer BackgroundObj;

	// Use this for initialization
	void Start () {
        Menus.ShopMenuData data = Menus.ShopMenuData.GetShopMenu();

        BackgroundObj.sprite = data.Backgrounds[SaveManager.savaData.SelectedBackground].HighRes;

        Common.ScaleSpriteToFitScreen(BackgroundObj,true);
	}
}
