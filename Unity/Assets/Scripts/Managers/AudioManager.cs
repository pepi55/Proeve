using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the ingame sounds
/// </summary>
public class AudioManager : MonoBehaviour
{

    /// <summary>
    /// AudioSource for the background music
    /// </summary>
    [SerializeField]
    AudioSource BGM = null;
    /// <summary>
    /// AudioSource for the Click sound effect
    /// </summary>
    [SerializeField]
    AudioSource SFX_Click = null;
    /// <summary>
    /// AudioSource for ball score sound effect
    /// </summary>
    [SerializeField]
    AudioSource SFX_BallScore = null;

    /// <summary>
    /// Audio clip for background music start
    /// </summary>
    [SerializeField]
    AudioClip BGM_Start = null;
    /// <summary>
    /// Audio clip for background music loop
    /// </summary>
    [SerializeField]
    AudioClip BGM_Loop = null;
    /// <summary>
    /// Audio clip for end of background music clip
    /// </summary>
    [SerializeField]
    AudioClip BGM_End = null;

    /// <summary>
    /// Audio clip for when the ball is clicked
    /// </summary>
    AudioClip SFX_ClickClip = null;
    /// <summary>
    /// Audio clip for when there is scored
    /// </summary>
    AudioClip SFX_BallScoreClip = null;

    /// <summary>
    /// All events are asinged here and audio clips for Click and ballscore are grabed from the shopmenuData
    /// </summary>
    void Start()
    {
        SFX_BallScoreClip = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].ScoreSound;
        SFX_ClickClip = Menus.ShopMenuData.GetShopMenu().Characters[SaveManager.savaData.SelectedCharacter].HitSound;

        if (SFX_ClickClip)
        {
            Events.GlobalEvents.AddEventListener<Events.IBallMove>(onBallClick);
        }
        if (SFX_BallScoreClip)
        {
            Events.GlobalEvents.AddEventListener<Events.IScore>(onBallScore);
        }

        StartCoroutine(startBGM());
        Events.GlobalEvents.AddEventListener<Events.IResetGameState>(onGameReset);

        Events.GlobalEvents.AddEventListener<Events.IBallHitBottom>(onBallhitGround);


    }

    void OnDestroy()
    {
        if (SFX_ClickClip)
        {
            Events.GlobalEvents.RemoveEventListener<Events.IBallMove>(onBallClick);
        }
        if (SFX_BallScoreClip)
        {
            Events.GlobalEvents.RemoveEventListener<Events.IScore>(onBallScore);
        }

        Events.GlobalEvents.RemoveEventListener<Events.IResetGameState>(onGameReset);

        Events.GlobalEvents.RemoveEventListener<Events.IBallHitBottom>(onBallhitGround);
    }

    void onBallClick(Events.IBallMove obj)
    {
        SFX_Click.PlayOneShot(SFX_ClickClip);
    }

    void onBallScore(Events.IScore obj)
    {
        SFX_BallScore.PlayOneShot(SFX_BallScoreClip);
    }

    void onGameReset(Events.IResetGameState obj)
    {
        if (obj.lastScoreWasHighScore)
        {
            StartCoroutine(startBGM());
        }
    }

    void onBallhitGround(Events.IBallHitBottom obj)
    {
        if (SaveManager.CheckNewScore(GameManager.Score))
        {
            StartCoroutine(playGameOver());
        }
    }

    /// <summary>
    /// Handels transition between STart clip and loop clip of the background music
    /// </summary>
    IEnumerator startBGM()
    {
        BGM.clip = BGM_Start;
        BGM.loop = false;
        BGM.Play();
        while (BGM.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        BGM.clip = BGM_Loop;
        BGM.loop = true;
        BGM.Play();
    }

    /// <summary>
    /// lowers the loop quickly then plays the game over sound
    /// </summary>
    IEnumerator playGameOver()
    {
        float vol =0.75f;
        while (vol > 0.1f)
        {
            vol -= Time.deltaTime * 4f;
            BGM.volume = vol;
            yield return new WaitForEndOfFrame();
        }

        BGM.Stop();

        BGM.clip = BGM_End;
        BGM.loop = false;
        BGM.Play();
        BGM.volume = 0.75f;


    }
}
