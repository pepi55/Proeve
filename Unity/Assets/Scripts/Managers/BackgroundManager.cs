using UnityEngine;
using System.Collections;
using Util;

/// <summary>
/// Manages the ingame background and gets the correct background
/// </summary>
public class BackgroundManager : MonoBehaviour {

    /// <summary>
    /// The sprite render that contains the background
    /// </summary>
    [SerializeField]
    SpriteRenderer BackgroundObj;

	// Use this for initialization
	void Start () {
        Menus.ShopMenuData data = Menus.ShopMenuData.GetShopMenu();

        BackgroundObj.sprite = data.Backgrounds[SaveManager.savaData.SelectedBackground].HighRes;

        Common.ScaleSpriteToFitScreen(BackgroundObj,true);
	}
}
