using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// Called when a click is registered on the screen
    /// </summary>
    public static event ClickDelegate onClick;
    /// <summary>
    /// Called when escape is pressed
    /// </summary>
    public static event VoidDelegate onEscapePress;

    public bool paused = false;

    void Awake()
    {
        Events.GlobalEvents.AddEventListener<Events.IPause>(OnPause);
    }

    public void OnDestroy()
    {
        Events.GlobalEvents.RemoveEventListener<Events.IPause>(OnPause);
    }

    void OnPause(Events.IPause pause)
    {
        paused = pause.State;
    }

    void Update()
    {
        if (!paused)
        {
            CheckTouches();
            CheckMouse();
        }
        KeyBoardChecks();
    }

    /// <summary>
    /// Check if there where touches on the screen. 
    /// wrote this so touches are also handeld
    /// </summary>
    void CheckTouches()
    {
        int count = Input.touchCount;
        Touch[] touches = Input.touches;

        for (int i = 0; i < count; i++)
        {
            if (touches[i].phase == TouchPhase.Ended)
            {
                OnClick(touches[i].position);
            }
        }
    }

    /// <summary>
    /// Check if the mouse button is down
    /// </summary>
    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick(Input.mousePosition);
        }
    }

    /// <summary>
    /// calls the onClick event
    /// </summary>
    /// <param name="Position">Position where the click happend</param>
    void OnClick(Vector2 Position)
    {
        if (onClick != null)
            onClick(Position);
    }

    /// <summary>
    /// Check the keyboard inputs
    /// </summary>
    void KeyBoardChecks()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onEscapePress != null)
            {
                onEscapePress();
            }
        }
    }
}
