using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    HighScoreMenu HighScoreScreen = null;
    [SerializeField]
    ShopMenu ShopMenuScreen = null;

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
        ShopMenuScreen.Close();        
        HighScoreScreen.onClose += Open;
        Close();
    }

    public void OpenStore()
    {
        ShopMenuScreen.Open();
        HighScoreScreen.Close();
        Close();

        ShopMenuScreen.onClose += Open;
    }

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
