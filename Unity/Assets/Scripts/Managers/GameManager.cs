using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

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
        instance = this;
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
