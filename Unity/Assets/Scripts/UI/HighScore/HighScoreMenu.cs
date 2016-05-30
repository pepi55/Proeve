using UnityEngine;
using System.Collections;

namespace Menus
{
    public class HighScoreMenu : BaseMenu
    {
        [SerializeField]
        private GameObject HighScoreObjectTemplate = null;
        [SerializeField]
        private Transform scoreBoard = null;

        [SerializeField]
        private Color HighScoreBackgroundColor1;
        [SerializeField]
        private Color HighScoreBackgroundColor2;

        private void Start()
        {
            HighScoreObjectTemplate.SetActive(false);
            MakeHighScoreList();
        }

        /// <summary>
        /// Fills the HighScoreMenu. Requires a template
        /// </summary>
        private void MakeHighScoreList()
        {
            GameObject g;
            HighScoreDisplayObject obj;
            bool dark = false;
            for (int i = 0; i < SaveManager.savaData.highScores.Length; i++)
            {
                //create highscore object
                g = Instantiate(HighScoreObjectTemplate) as GameObject;
                g.SetActive(true);
                g.transform.SetParent(scoreBoard, false);
                obj = g.GetComponent<HighScoreDisplayObject>();

                //the the values of the display object
                obj.SetValues(SaveManager.savaData.highScores[i].name, SaveManager.savaData.highScores[i].score.ToString(), dark ? Color.gray : Color.white);
                obj.Score.color = obj.Name.color = dark ? HighScoreBackgroundColor1 : HighScoreBackgroundColor2;
                dark = !dark;
            }
        }

    }
}