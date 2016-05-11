using UnityEngine;
using System.Collections;
using Events;
using System;

/// <summary>
/// Pause Menu Opens when the player pauses the game
/// </summary>
public class PauseMenu : MonoBehaviour
{
    void Awake()
    {
        Events.GlobalEvents.AddEventListener<Events.IPause>(OnPause);
    }

    /// <summary>
    /// Triggerd when the game is paused
    /// </summary>
    /// <param name="obj">contains pause state</param>
    private void OnPause(IPause obj)
    {
        gameObject.SetActive(obj.State);
    }

    /// <summary>
    /// Close the pause menu
    /// </summary>
    public void Close()
    {
        GlobalEvents.Invoke(new IPause(false));
    }

    public void OnDestroy()
    {
        //clean up that event listener or else bad things happen...
        Events.GlobalEvents.RemoveEventListener<IPause>(OnPause);
    }

    public void BackToMainMenu()
    {
        Util.SceneControler.Load("MainMenu");
    }
}
