using UnityEngine;
using System.Collections;
namespace Menus
{
    public class HighScoreSubmitScreen : MonoBehaviour
    {
        public event VoidDelegate onClose;

        [SerializeField]
        UnityEngine.UI.InputField inputfield = null;
        [SerializeField]
        UnityEngine.UI.Text ScoreDisplay = null;

        public void Open()
        {
            if (gameObject)
            {
                gameObject.SetActive(true);
            }
            ScoreDisplay.text = "Congratulations you scored " + GameManager.Score.ToString() + " points!";
        }

        public void Submit()
        {
            SaveManager.AddNewScore(GameManager.Score, inputfield.text);
            Close();
        }

        public void Cancel()
        {
            Close();
        }

        public void Close()
        {
            if (onClose != null)
            {
                onClose();
                onClose = null;
            }
            gameObject.SetActive(false);
        }
    }
}