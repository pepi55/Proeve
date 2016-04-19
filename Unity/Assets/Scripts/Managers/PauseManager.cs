using UnityEngine;
using System.Collections;
using Events;
using System;

public class PauseManager : MonoBehaviour
{

    void Awake()
    {
        Events.GlobalEvents.AddEventListener<Events.IPause>(OnPause);
    }

    private void OnPause(IPause obj)
    {
        gameObject.SetActive(obj.State);
    }

    public void Close()
    {
        GlobalEvents.Invoke(new IPause(false));
    }

    public void OnDestroy()
    {
        Events.GlobalEvents.RemoveEventListener<IPause>(OnPause);
    }

    public void BackToMainMenu()
    {
        Util.SceneControler.Load("MainMenu");
    }
}
