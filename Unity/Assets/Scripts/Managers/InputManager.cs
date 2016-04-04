using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public static event ClickDelegate onClick;

    void Start()
    {
        Events.GlobalEvents.AddEventListener<Events.IPause>(OnPause);
    }

    void OnPause(Events.IPause pause)
    {
        enabled = !pause.State;
    }

    void Update()
    {
        CheckTouches();
        CheckMouse();
    }

    void CheckTouches()
    {
        int count = Input.touchCount;
        Touch[] touches = Input.touches;

        for (int i = 0; i < count; i++)
        {
            OnClick(touches[i].position);
        }
    }

    void CheckMouse()
    {
        if (Input.GetMouseButton(0))
        {
            OnClick(Input.mousePosition);
        }
    }

    void OnClick(Vector2 Position)
    {
        if (onClick != null)
            onClick(Position);
    }
}
