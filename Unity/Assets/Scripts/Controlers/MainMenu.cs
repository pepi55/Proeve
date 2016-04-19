using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    HighScoreMenu HighScoreScreen = null;

    void Awake()
    {
        Open();
        HighScoreScreen.Close();
    }

	public void StartGame()
    {
        Util.SceneControler.Load("MainGame");
    }

    public void OpenHighScore()
    {
        HighScoreScreen.Open();
        Close();
        HighScoreScreen.onClose += Open;
    }

    public void OpenStore()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
