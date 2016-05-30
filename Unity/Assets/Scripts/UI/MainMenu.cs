using System.Collections;
using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        HighScoreMenu HighScoreScreen = null;
        [SerializeField]
        ShopMenu ShopMenuScreen = null;

        void Awake()
        {
            Open();
            HighScoreScreen.Close();
        }

        /// <summary>
        /// Loads the main game scene
        /// </summary>
        public void StartGame()
        {
            Util.SceneControler.Load("MainGame");
        }

        /// <summary>
        /// Opens the highscore screen and closes all other open menus
        /// </summary>
        public void OpenHighScore()
        {
            HighScoreScreen.Open();
            ShopMenuScreen.Close();
            HighScoreScreen.onClose += Open;
            Close();
        }

        /// <summary>
        /// Opens menu for Store
        /// </summary>
        public void OpenStore()
        {
            ShopMenuScreen.Open();
            HighScoreScreen.Close();
            Close();

            ShopMenuScreen.onClose += Open;
        }

        /// <summary>
        /// Opens the menu and closes the others
        /// </summary>
        public void Open()
        {
            gameObject.SetActive(true);

            ShopMenuScreen.Close();
            HighScoreScreen.Close();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}