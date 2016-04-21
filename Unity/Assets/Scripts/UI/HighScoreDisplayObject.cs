using UnityEngine;
using System.Collections;

public class HighScoreDisplayObject : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text score = null, _name = null;
    public UnityEngine.UI.Text Name { get { return _name; } }
    public UnityEngine.UI.Text Score { get { return score; } }
    [SerializeField]
    UnityEngine.UI.Image Background = null;

    public void SetValues(string Name, string Score, Color backgroundColor)
    {
        this.score.text = Score;
        this._name.text = Name;
        Background.color = backgroundColor;
    }
}
