using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopMenu : BaseMenu
{
    ShopMenuData data;

    List<MenuItem> Characters;
    List<MenuItem> Backgrounds;
    
    [SerializeField]
    GameObject PrefabDisplayObject;

    [Header("Selection")]
    [SerializeField]
    RectTransform CharacterContent;
    [SerializeField]
    RectTransform BackgroundContent;
    [SerializeField]
    GameObject BackgroundGameObj, CharacterGameObj;

    [Header("Navigation and indicators")]
    [SerializeField]
    Button BackgroundButton;
    [SerializeField]
    Button CharacterButton;

    [SerializeField]
    Text StorePointsIndicator;

   private void Awake()
    {
        GameObject temp = Instantiate(Resources.Load("ShopData")) as GameObject;
        data = temp.GetComponent<ShopMenuData>();

        if(SaveManager.savaData.UnlockedCharacters == null)
        {
            SaveManager.savaData.UnlockedCharacters = new bool[0];
        }

        if(SaveManager.savaData.UnlockedBackgrounds == null)
        {
            SaveManager.savaData.UnlockedBackgrounds = new bool[0];
        }

        if(SaveManager.savaData.UnlockedCharacters.Length != data.Characters.Length)
        {
            SaveManager.savaData.UnlockedCharacters = (bool[])Util.Common.ResizeArray(SaveManager.savaData.UnlockedCharacters, new int[] { data.Characters.Length });
        }

        if(SaveManager.savaData.UnlockedBackgrounds.Length != data.Backgrounds.Length)
        {
            SaveManager.savaData.UnlockedBackgrounds = (bool[])Util.Common.ResizeArray(SaveManager.savaData.UnlockedBackgrounds, new int[] { data.Backgrounds.Length });
        }

        InitCharacters();
        InitBackgrounds();

        UpdateDisplayList(Characters, SaveManager.savaData.SelectedCharacter);
        UpdateDisplayList(Backgrounds, SaveManager.savaData.SelectedBackground);
    }

    public override void Open()
    {
        base.Open();

        UpdateStorePointText();
        OpenCharacter();
        if (data.Characters.Length > 0)
        {
            CharacterGameObj.GetComponentInChildren<ScrollRect>().horizontalNormalizedPosition = (float)SaveManager.savaData.SelectedCharacter / data.Characters.Length;
        }

        if (data.Backgrounds.Length > 0)
        {
            BackgroundGameObj.GetComponentInChildren<ScrollRect>().horizontalNormalizedPosition = (float)SaveManager.savaData.SelectedBackground / data.Backgrounds.Length;
        }
    }

    public void OpenCharacter()
    {
        CharacterGameObj.SetActive(true);
        BackgroundGameObj.SetActive(false);
        CharacterButton.interactable = false;
        BackgroundButton.interactable = true;
    }

    public void OpenBackgrounds()
    {
        CharacterGameObj.SetActive(false);
        BackgroundGameObj.SetActive(true);
        CharacterButton.interactable = true;
        BackgroundButton.interactable = false;
    }

    void InitCharacters()
    {

        Characters = new List<MenuItem>();
        int l = data.Characters.Length;

        GameObject gameObj;
        MenuItem MI;
        CharacterContent.sizeDelta = new Vector2(CharacterContent.sizeDelta.x * l, CharacterContent.sizeDelta.y);
        for (int i = 0; i < l; i++)
        {
            //setting up template Object
            gameObj = Instantiate(PrefabDisplayObject);
            gameObj.SetActive(true);
            gameObj.transform.SetParent(CharacterContent, false);

            //setting up menu item
            MI = new MenuItem(i, gameObj, data.Characters);

            MI.setupButtonCharacter();

            if(data.Characters[i].Cost <= 0)
            {
                SaveManager.savaData.UnlockedCharacters[i] = true;
            }

            if(SaveManager.savaData.UnlockedCharacters[i])
            {
                MI.HasBeenUnlocked();
            }

            Characters.Add(MI);
        }       
    }

    void InitBackgrounds()
    {
        Backgrounds = new List<MenuItem>();
        int l = data.Backgrounds.Length;

        GameObject gameObj;
        MenuItem MI;

        for (int i = 0; i < l; i++)
        {
            //setting up template Object
            gameObj = Instantiate(PrefabDisplayObject);
            gameObj.SetActive(true);
            gameObj.transform.SetParent(BackgroundContent, false);

            //setting up menu item
            MI = new MenuItem(i, gameObj, data.Backgrounds);

            MI.setupButtonBackgrounds();

            if (data.Backgrounds[i].Cost <= 0)
            {
                SaveManager.savaData.UnlockedBackgrounds[i] = true;
            }

            if (SaveManager.savaData.UnlockedBackgrounds[i])
            {
                MI.HasBeenUnlocked();
            }

            Backgrounds.Add(MI);
        }

    }

    public void ClickCharacter(int Index)
    {
        if (!SaveManager.savaData.UnlockedCharacters[Index])
        {
            if (SaveManager.savaData.StorePoints >= data.Characters[Index].Cost)
            {
                SaveManager.savaData.UnlockedCharacters[Index] = true;
                SaveManager.savaData.StorePoints -= data.Characters[Index].Cost;
                UpdateStorePointText();
                Characters[Index].HasBeenUnlocked();
            }
            else
            {
                return;
            }
        }

        SaveManager.savaData.SelectedCharacter = Index;
        SaveManager.Save();

        UpdateDisplayList(Characters, Index);
    }

    public void ClickBackground(int Index)
    {
        if (!SaveManager.savaData.UnlockedBackgrounds[Index])
        {
            if(SaveManager.savaData.StorePoints >= data.Backgrounds[Index].Cost)
            {
                SaveManager.savaData.UnlockedBackgrounds[Index] = true;
                SaveManager.savaData.StorePoints -= data.Backgrounds[Index].Cost;
                UpdateStorePointText();
                Backgrounds[Index].HasBeenUnlocked();
            }
            else
            {
                return;
            }
        }

        SaveManager.savaData.SelectedBackground = Index;
        SaveManager.Save();

        UpdateDisplayList(Backgrounds, Index);
    }

    void UpdateDisplayList(List<MenuItem> list, int SelectedItemIndex)
    {
        if (list.Count == 0)
            return;
        foreach (MenuItem MI in list)
        {
            if (MI.indexPosition == SelectedItemIndex)
            {
                MI.SelectorImage.color = Color.white;
            }
            else
            {
                MI.SelectorImage.color = Color.gray;
            }
        }
    }

    void UpdateStorePointText()
    {
        StorePointsIndicator.text = "You have " + SaveManager.savaData.StorePoints + " left to spent";
    }

    class MenuItem
    {
        public Button SelectButton;
        public Image SelectorImage;
        public GameObject LockObj;

        public int indexPosition;

        /// <summary>
        /// Init for Menu Item
        /// </summary>
        /// <param name="indexPosition">the position in the array</param>
        /// <param name="gameObj">game object that contains all visual elements</param>
        /// <param name="data">Array that contains all data to build up a menu Item</param>
        public MenuItem (int indexPosition, GameObject gameObj, ShopMenuData.StoreObject[] data)
        {
            this.indexPosition = indexPosition;

            //Setting up images
            Image[] images = gameObj.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                switch (img.name)
                {
                    case "previewImg":
                        img.sprite = data[indexPosition].HighRes;
                        break;

                    case "selector":
                        this.SelectorImage = img;
                        break;

                    case "lockObj":
                        this.LockObj = img.gameObject;
                        break;
                }
            }

            //setting up texts
            Text[] textObjs = gameObj.GetComponentsInChildren<Text>();
            foreach (Text tex in textObjs)
            {
                switch (tex.name)
                {
                    case "cost":
                        tex.text = "Requires " + data[indexPosition].Cost + " points to buy";
                        break;

                    case "name":
                        break;
                }
            }

            this.SelectButton = gameObj.GetComponentInChildren<Button>();
        }

        public void setupButtonCharacter()
        {
            SelectButton.onClick.AddListener(() =>
            {
                ShopMenu m = FindObjectOfType<ShopMenu>();
                m.ClickCharacter(indexPosition);
            });
        }

        public void setupButtonBackgrounds()
        {
            SelectButton.onClick.AddListener(() =>
            {
                ShopMenu m = FindObjectOfType<ShopMenu>();
                m.ClickBackground(indexPosition);
            });
        }

        public void HasBeenUnlocked()
        {
            LockObj.gameObject.SetActive(false);
        }
    }
}

