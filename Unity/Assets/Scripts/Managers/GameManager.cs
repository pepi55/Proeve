using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    //needed a private instance for score. 
    private static GameManager instance;

    //used to trigger the UI or anything else that needs to be update when the score changes
    public static event VoidDelegate OnScoreUpdate;

    [SerializeField]
    private int score;

    /// <summary>
    /// The Points earned of the player
    /// </summary>
    public static int Score
    {
        get
        {
            return instance.score;
        }
    }

    void Awake()
    {
        score = 0;
        instance = this;
        Events.GlobalEvents.AddEventListener<Events.IScore>(AddPoint);
        Events.GlobalEvents.AddEventListener<Events.IPlayerHitBottom>(BallHitGround);
    }

    /// <summary>
    /// Event Handler for IScore
    /// </summary>
    /// <param name="obj">object argement is not releivant for this event as it does not contain anything</param>
    private void AddPoint(Events.IScore obj)
    {
        score++;

        if (OnScoreUpdate != null)
            OnScoreUpdate();
    }

    private void BallHitGround(Events.IPlayerHitBottom obj)
    {
        if (SaveManager.CheckNewScore(score))
        {

        }
        else
        {
            ResetScore();
        }
        
        
    }

    private void OpenScoreSubmitScreen()
    {

    }

    private void ResetScore()
    {
        score = 0;

        if (OnScoreUpdate != null)
            OnScoreUpdate();
    }

    /// <summary>
    /// Call to unpause game
    /// </summary>
    public void ContinueGame()
    {
        Events.GlobalEvents.Invoke(new Events.IPause(false));
    }

    /// <summary>
    /// Call to pause game
    /// </summary>
    public void PauseGame()
    {
        Events.GlobalEvents.Invoke(new Events.IPause(true));
    }
}

[System.Serializable]
class SaveTest
{
    public string TEST = "";
}
