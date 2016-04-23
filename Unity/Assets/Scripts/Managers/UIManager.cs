using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    Text ScoreText = null;

    void Awake()
    {
        GameManager.OnScoreUpdate += GameManager_OnScoreUpdate;

        ScoreText.text = GameManager.Score.ToString();
    }

    public void OnDestroy()
    {
        GameManager.OnScoreUpdate -= GameManager_OnScoreUpdate;
    }



    /// <summary>
    /// Called after the score number is changed.
    /// </summary>
    private void GameManager_OnScoreUpdate()
    {
        ScoreText.text = GameManager.Score.ToString();
    }
}
