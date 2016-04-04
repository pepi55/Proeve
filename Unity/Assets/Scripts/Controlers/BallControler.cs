using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallControler : MonoBehaviour
{

    new Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start()
    {
        InputManager.onClick += InputManager_onClick;

        Events.GlobalEvents.AddEventListener<Events.IPause>(OnPause);

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnPause(Events.IPause pause)
    {
        enabled = !pause.State;
    }

    private void InputManager_onClick(Vector2 position)
    {
        if(isActiveAndEnabled)
        {
            //TODO Execute action Code here
        }
    }
}
