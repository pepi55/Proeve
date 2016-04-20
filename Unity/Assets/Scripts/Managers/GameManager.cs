using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    //needed a private instance for score. 
    private static GameManager instance;

    //used to trigger the UI or anything else that needs to be update when the score changes
    public static event VoidDelegate OnScoreUpdate;

    [SerializeField]
    private int score;

    [SerializeField]
    HighScoreSubmitScreen submitMenu = null;

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

    /// <summary>
    /// Show that game is paused
    /// </summary>
    public static bool GamePaused
    {
        get { return instance.gamePaused; }
    }

    private bool gamePaused;
    /// <summary>
    /// if is waiting for High score submision
    /// </summary>
    private bool waitForSubmit;

    void Awake()
    {
        score = 0;
        instance = this;
        Events.GlobalEvents.AddEventListener<Events.IScore>(AddPoint);
        Events.GlobalEvents.AddEventListener<Events.IPlayerHitBottom>(BallHitGround);
    }

    void Start()
    {
        submitMenu.Close();
        ContinueGame();
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

    /// <summary>
    /// Event Handler for IPlayerHitBottom
    /// </summary>
    /// <param name="obj">Object Argument does not contain anyvalue only used as identifier</param>
    private void BallHitGround(Events.IPlayerHitBottom obj)
    {
        if (SaveManager.CheckNewScore(score) && score != 0)
        {
            OpenScoreSubmitScreen();

            gamePaused = true;
            waitForSubmit = true;
        }
        else
        {
            ResetGame();
            gamePaused = false;
            
        }  
    }

    private void OpenScoreSubmitScreen()
    {
        submitMenu.Open();
        submitMenu.onClose += ResetGame;
    }

    private void ResetGame()
    {
        score = 0;
        if (waitForSubmit)
        {
            gamePaused = false;
        }

        waitForSubmit = false;

        if (OnScoreUpdate != null)
            OnScoreUpdate();

        Events.GlobalEvents.Invoke<Events.IResetGameState>(new Events.IResetGameState());
    }

    /// <summary>
    /// Call to unpause game
    /// </summary>
    public void ContinueGame()
    {
        if(waitForSubmit)
        {
            return;
        }
        Events.GlobalEvents.Invoke(new Events.IPause(false));
        gamePaused = false;
    }

    /// <summary>
    /// Call to pause game
    /// </summary>
    public void PauseGame()
    {
        if (waitForSubmit)
        {
            return;
        }

        Events.GlobalEvents.Invoke(new Events.IPause(true));
        gamePaused = true;
    }
}