using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopMenu : MonoBehaviour
{
    
    ShopMenuData data;

    List<MenuItem> Characters;
    List<MenuItem> Backgrounds;

    [SerializeField]
    GameObject PrefabDisplayObject;

    [SerializeField]
    RectTransform CharacterDisplayParent;
    [SerializeField]
    RectTransform BackgroundDisplayParent;

   private void Start()
    {
        GameObject temp = Instantiate(Resources.Load("ShopData")) as GameObject;
        data = temp.GetComponent<ShopMenuData>();

        InitCharacters();
    }

    void InitCharacters()
    {

        Characters = new List<MenuItem>();
        int l = data.Characters.Length;

        GameObject gameObj;
        MenuItem MI;
        CharacterDisplayParent.sizeDelta = new Vector2(CharacterDisplayParent.sizeDelta.x * l, CharacterDisplayParent.sizeDelta.y);
        for (int i = 0; i < l; i++)
        {

            MI = new MenuItem(i);
            gameObj = Instantiate(PrefabDisplayObject);

            Image[] images = gameObj.GetComponentsInChildren<Image>();
            foreach(Image img in images)
            {
                if(img.name == "previewImg")
                {
                    img.sprite = data.Characters[i];
                }
                else if(img.name == "selector")
                {
                    MI.Selector = img;
                }
            }

            MI.SelectButton = gameObj.GetComponentInChildren<Button>();
            MI.setupButton();

            Characters.Add(MI);

            gameObj.transform.SetParent(CharacterDisplayParent, false);
        }
    }

    public void ClickCharacter(int Index)
    {
        SaveManager.savaData.SelectedCharacter = Index;

        foreach(MenuItem MI in Characters)
        {
            if(MI.indexPosition == Index)
            {
                MI.Selector.color = Color.white;
            }
            else
            {
                MI.Selector.color = Color.gray;
            }
        }
    }

    public void ClickBackground(int Index)
    {
        SaveManager.savaData.SelectedBackground = Index;
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

        public void setupButton()
        {
            SelectButton.onClick.AddListener(() =>
            {
                ShopMenu m = FindObjectOfType<ShopMenu>();
                m.ClickCharacter(indexPosition);
            });
        }
    }
}

