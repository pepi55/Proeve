using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{


    [SerializeField]
    AudioSource BGM = null;
    [SerializeField]
    AudioSource SFX_Click = null;
    [SerializeField]
    AudioSource SFX_BallScore = null;

    [SerializeField]
    AudioClip BGM_Start = null;
    [SerializeField]
    AudioClip BGM_Loop = null;
    [SerializeField]
    AudioClip BGM_End = null;

    AudioClip SFX_ClickClip = null;
    AudioClip SFX_BallScoreClip = null;

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

    IEnumerator playGameOver()
    {
        float vol = 1f;
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
        BGM.volume = 1f;


    }
}
