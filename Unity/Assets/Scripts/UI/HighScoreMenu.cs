using UnityEngine;
using System.Collections;

public class HighScoreMenu : MonoBehaviour
{

    public VoidDelegate onClose;

    [SerializeField]
    GameObject HighScoreObjectTemplate = null;
    [SerializeField]
    Transform scoreBoard = null;

    void Start()
    {
        GameObject g;
        bool dark = false;
        for (int i = 0; i < SaveManager.savaData.highScores.Length; i++)
        {
            g = Instantiate(HighScoreObjectTemplate) as GameObject;
            g.SetActive(true);
            g.transform.SetParent(scoreBoard,false);
            g.GetComponent<HighScoreDisplayObject>().SetValues(SaveManager.savaData.highScores[i].name, SaveManager.savaData.highScores[i].score.ToString(), dark ? Color.gray : Color.white);
            dark = !dark;
        }

        HighScoreObjectTemplate.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if (onClose != null)
            onClose();
        onClose = null;
    }
}
