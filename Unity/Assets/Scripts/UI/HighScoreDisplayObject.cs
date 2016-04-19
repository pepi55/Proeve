using UnityEngine;
using System.Collections;

public class HighScoreDisplayObject : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text Score = null, Name = null;
    [SerializeField]
    UnityEngine.UI.Image Background = null;

    public void SetValues(string Name, string Score, Color backgroundColor)
    {
        this.Score.text = Score;
        this.Name.text = Name;
        Background.color = backgroundColor;
    }
}
