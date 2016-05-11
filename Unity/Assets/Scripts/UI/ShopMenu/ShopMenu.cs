using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Menus
{
    public class ShopMenu : BaseMenu
    {
        ShopMenuData data;

        List<MenuItem> Characters = null;
        List<MenuItem> Backgrounds = null;

        [SerializeField]
        GameObject PrefabDisplayObject = null;

        [Header("Selection")]
        [SerializeField]
        RectTransform CharacterContent = null;
        [SerializeField]
        RectTransform BackgroundContent = null;
        [SerializeField]
        GameObject BackgroundGameObj = null, CharacterGameObj = null;

        [Header("Navigation and indicators")]
        [SerializeField]
        Button BackgroundButton = null;
        [SerializeField]
        Button CharacterButton = null;

        [SerializeField]
        Text StorePointsIndicator = null;

        private void Awake()
        {
            GameObject temp = Instantiate(Resources.Load("ShopData")) as GameObject;
            data = temp.GetComponent<ShopMenuData>();

            //check if unlock data exists
            if (SaveManager.savaData.UnlockedCharacters == null)
            {
                SaveManager.savaData.UnlockedCharacters = new bool[0];
            }

            if (SaveManager.savaData.UnlockedBackgrounds == null)
            {
                SaveManager.savaData.UnlockedBackgrounds = new bool[0];
            }

            //check if lenght is the same as the number of characters
            if (SaveManager.savaData.UnlockedCharacters.Length != data.Characters.Length)
            {
                SaveManager.savaData.UnlockedCharacters = (bool[])Util.Common.ResizeArray(SaveManager.savaData.UnlockedCharacters, new int[] { data.Characters.Length });
            }

            if (SaveManager.savaData.UnlockedBackgrounds.Length != data.Backgrounds.Length)
            {
                SaveManager.savaData.UnlockedBackgrounds = (bool[])Util.Common.ResizeArray(SaveManager.savaData.UnlockedBackgrounds, new int[] { data.Backgrounds.Length });
            }

            InitCharacters();
            InitBackgrounds();

            UpdateDisplayList(Characters, SaveManager.savaData.SelectedCharacter);
            UpdateDisplayList(Backgrounds, SaveManager.savaData.SelectedBackground);

            PrefabDisplayObject.SetActive(false);
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

        /// <summary>
        /// Opens character selection menu
        /// </summary>
        public void OpenCharacter()
        {
            CharacterGameObj.SetActive(true);
            BackgroundGameObj.SetActive(false);
            CharacterButton.interactable = false;
            BackgroundButton.interactable = true;
        }

        /// <summary>
        /// Opens background selection menu
        /// </summary>
        public void OpenBackgrounds()
        {
            CharacterGameObj.SetActive(false);
            BackgroundGameObj.SetActive(true);
            CharacterButton.interactable = true;
            BackgroundButton.interactable = false;
        }

        /// <summary>
        /// Init for character menu. 
        /// This funtion creates the buttons that can be pressed to select a character
        /// </summary>
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

                //auto unlock character if cost is zero or smaller
                if (data.Characters[i].Cost <= 0)
                {
                    SaveManager.savaData.UnlockedCharacters[i] = true;
                }

                //check if character has been unlocked and if it is then the lock image will be disabled
                if (SaveManager.savaData.UnlockedCharacters[i])
                {
                    MI.HasBeenUnlocked();
                }

                //add character to list that contains all the display classes for the character selection screen
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

                //auto unlock if background cost zero or less
                if (data.Backgrounds[i].Cost <= 0)
                {
                    SaveManager.savaData.UnlockedBackgrounds[i] = true;
                }

                //check if background is unlocked if it is then run the hasbeenUnlock funtion
                if (SaveManager.savaData.UnlockedBackgrounds[i])
                {
                    MI.HasBeenUnlocked();
                }

                //add background object to list that contains the value classes of all background selection buttons
                Backgrounds.Add(MI);
            }

        }

        /// <summary>
        /// Handels clicks on a character and checks if it has been unlocked and if it is then it will select that character
        /// Else 
        /// </summary>
        /// <param name="Index">Index of the item that clicked</param>
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

        /// <summary>
        /// Handels Clicks for the Backgrounds.
        /// Also checks if a background as been unlocked
        /// </summary>
        /// <param name="Index">Index of the item that was clicked on</param>
        public void ClickBackground(int Index)
        {
            if (!SaveManager.savaData.UnlockedBackgrounds[Index])
            {
                if (SaveManager.savaData.StorePoints >= data.Backgrounds[Index].Cost)
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

        /// <summary>
        /// Updates the display list, it changes the visuale of the selected object. So the user knows what they selected
        /// </summary>
        /// <param name="list">The list that contains the selected item</param>
        /// <param name="SelectedItemIndex">Location of the selected item</param>
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

        /// <summary>
        /// Updates text field that contains how much points you have
        /// </summary>
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
            public MenuItem(int indexPosition, GameObject gameObj, ShopMenuData.StoreObject[] data)
            {
                gameObj.SetActive(true);
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
                            //TODO implement name into the game object
                            break;
                    }
                }

                this.SelectButton = gameObj.GetComponentInChildren<Button>();
            }

            /// <summary>
            /// Sets the button up as a Character selection button
            /// </summary>
            public void setupButtonCharacter()
            {
                SelectButton.onClick.AddListener(() =>
                {
                //Did it like this because is was getting null pointers if i did it on any otherway
                ShopMenu m = FindObjectOfType<ShopMenu>();
                    m.ClickCharacter(indexPosition);
                });
            }

            /// <summary>
            /// Sets the button up as a Background selection button
            /// </summary>
            public void setupButtonBackgrounds()
            {
                SelectButton.onClick.AddListener(() =>
                {
                    ShopMenu m = FindObjectOfType<ShopMenu>();
                    m.ClickBackground(indexPosition);
                });
            }

            /// <summary>
            /// Called when the background or character has been unlocked
            /// This only changes the visual look of the button
            /// </summary>
            public void HasBeenUnlocked()
            {
                LockObj.gameObject.SetActive(false);
            }
        }
    }
}