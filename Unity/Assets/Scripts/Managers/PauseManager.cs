using UnityEngine;
using System.Collections;
using Events;
using System;

public class PauseManager : MonoBehaviour {

    void Awake()
    {
        Events.GlobalEvents.AddEventListener<Events.IPause>(onPause);
    }

    private void onPause(IPause obj)
    {
        gameObject.SetActive(obj.State);
    }

    public 
}
