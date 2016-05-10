using UnityEngine;
using System.Collections;

namespace Menus
{
    public class HighScoreMenu : BaseMenu
    {
        [SerializeField]
        GameObject HighScoreObjectTemplate = null;
        [SerializeField]
        Transform scoreBoard = null;

        void Start()
        {
            HighScoreObjectTemplate.SetActive(false);
        }

        /// <summary>
        /// Fills the HighScoreMenu. Requires a template
        /// </summary>
        void MakeHighScoreList()
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
                obj.Score.color = obj.Name.color = dark ? (Color.grey + (Color.white / 4f)) : Color.black;
                dark = !dark;
            }
        }

    }
}