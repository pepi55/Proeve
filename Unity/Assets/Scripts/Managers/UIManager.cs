using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Controles the ingame UI
/// </summary>
public class UIManager : MonoBehaviour
{

    [SerializeField]
    Text ScoreText = null;

    float ScoreTextScaleMult = 1f;

    void Awake()
    {
        GameManager.OnScoreUpdate += GameManager_OnScoreUpdate;

        ScoreText.text = GameManager.Score.ToString();
    }

    public void OnDestroy()
    {
        GameManager.OnScoreUpdate -= GameManager_OnScoreUpdate;
    }

    void Update()
    {
        if (ScoreTextScaleMult != 1f)
        {
            if (ScoreTextScaleMult > 1f)
            {
                ScoreText.transform.localScale = Vector3.one * ScoreTextScaleMult;
                ScoreTextScaleMult -= Time.deltaTime * 2f;
            }
            else if (ScoreTextScaleMult < 1f)
            {
                ScoreTextScaleMult = 1f;
                ScoreText.transform.localScale = Vector3.one;
            }
        }
    }

    /// <summary>
    /// Called after the score number is changed.
    /// </summary>
    private void GameManager_OnScoreUpdate()
    {
        ScoreText.text = GameManager.Score.ToString();
        ScoreTextScaleMult = 1.3f;
    }
}
