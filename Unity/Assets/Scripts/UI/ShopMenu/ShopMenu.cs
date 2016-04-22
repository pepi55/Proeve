using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopMenu : MonoBehaviour
{

    public VoidDelegate onClose;

    ShopMenuData data;

    List<MenuItem> Characters;
    List<MenuItem> Backgrounds;

    [SerializeField]
    GameObject PrefabDisplayObject;

    [SerializeField]
    RectTransform CharacterContent, BackgroundContent;

    [SerializeField]
    GameObject BackgroundGameObj, CharacterGameObj;

    [SerializeField]
    Button BackgroundButton, CharacterButton;

   private void Start()
    {
        GameObject temp = Instantiate(Resources.Load("ShopData")) as GameObject;
        data = temp.GetComponent<ShopMenuData>();

        InitCharacters();
        InitBackgrounds();

        UpdateDisplayList(Characters, SaveManager.savaData.SelectedCharacter);
        UpdateDisplayList(Backgrounds, SaveManager.savaData.SelectedBackground);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        OpenCharacter();
    }

    public void Close()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        gameObject.SetActive(false);

        if (onClose != null)
        {
            onClose();
            onClose = null;
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
        int l = data.CharacterPreviewImages.Length;

        GameObject gameObj;
        MenuItem MI;
        CharacterContent.sizeDelta = new Vector2(CharacterContent.sizeDelta.x * l, CharacterContent.sizeDelta.y);
        for (int i = 0; i < l; i++)
        {

            MI = new MenuItem(i);
            gameObj = Instantiate(PrefabDisplayObject);
            gameObj.SetActive(true);

            Image[] images = gameObj.GetComponentsInChildren<Image>();
            foreach(Image img in images)
            {
                if(img.name == "previewImg")
                {               
                    img.sprite = data.CharacterPreviewImages[i];
                }
                else if(img.name == "selector")
                {
                    MI.Selector = img;
                }
            }

            MI.SelectButton = gameObj.GetComponentInChildren<Button>();
            MI.setupButtonCharacter();

            Characters.Add(MI);

            gameObj.transform.SetParent(CharacterContent, false);
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

            MI = new MenuItem(i);

            gameObj = Instantiate(PrefabDisplayObject);
            gameObj.SetActive(true);

            Image[] images = gameObj.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                if (img.name == "previewImg")
                {
                    img.sprite = data.Backgrounds[i];
                }
                else if (img.name == "selector")
                {
                    MI.Selector = img;
                }
            }

            MI.SelectButton = gameObj.GetComponentInChildren<Button>();
            MI.setupButtonCharacter();

            Backgrounds.Add(MI);

            gameObj.transform.SetParent(BackgroundContent, false);
        }

    }

    public void ClickCharacter(int Index)
    {
        SaveManager.savaData.SelectedCharacter = Index;
        SaveManager.Save();

        UpdateDisplayList(Characters, Index);
    }

    

    public void ClickBackground(int Index)
    {
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
                MI.Selector.color = Color.white;
            }
            else
            {
                MI.Selector.color = Color.gray;
            }
        }
    }
    class MenuItem
    {
        public Button SelectButton;
        public Image Selector;

        public int indexPosition;

        public MenuItem (int indexPosition)
        {
            this.indexPosition = indexPosition;
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
    }
}

