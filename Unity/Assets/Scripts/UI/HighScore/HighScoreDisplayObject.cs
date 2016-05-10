using UnityEngine;
using System.Collections;

/// <summary>
/// A display object for in the highscore screen
/// </summary>
public class HighScoreDisplayObject : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text _name = null;
    public UnityEngine.UI.Text Name { get { return _name; } }

    [SerializeField]
    UnityEngine.UI.Text score = null;
    public UnityEngine.UI.Text Score { get { return score; } }

    [SerializeField]
    UnityEngine.UI.Image Background = null;

    /// <summary>
    /// Sets the value that will be displayed
    /// </summary>
    /// <param name="Name">Name of the player that submited the score</param>
    /// <param name="Score">Score the player got</param>
    /// <param name="backgroundColor">The color the background will be having</param>
    public void SetValues(string Name, string Score, Color backgroundColor)
    {
        this.score.text = Score;
        this._name.text = Name;
        Background.color = backgroundColor;
    }
}
