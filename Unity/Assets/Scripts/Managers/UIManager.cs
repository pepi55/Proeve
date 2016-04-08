using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [SerializeField]
    Text ScoreText;

	void Awake()
    {
        GameManager.OnScoreUpdate += GameManager_OnScoreUpdate;

        ScoreText.text = GameManager.Score.ToString();
    }

    private void GameManager_OnScoreUpdate()
    {
        ScoreText.text = GameManager.Score.ToString();
    }
}
