using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public static event VoidDelegate OnScoreUpdate;

    private int score;

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
    }

    private void AddPoint(Events.IScore obj)
    {
        score++;

        if (OnScoreUpdate != null)
            OnScoreUpdate();
    }

    public void ContinueGame()
    {
        Events.GlobalEvents.Invoke(new Events.IPause(false));
    }

    public void PauseGame()
    {
        Events.GlobalEvents.Invoke(new Events.IPause(true));
    }
}
