using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallControler : MonoBehaviour
{

    new Rigidbody2D rigidbody2D;

    bool useLocalRelativePosition;
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
        if (isActiveAndEnabled)
        {
            if (transform.position.y < 12f)
            {
                Vector2 dir;
                if (!useLocalRelativePosition)
                {
                    dir = position - new Vector2(Screen.width / 2f, Screen.height / 2f);
                    dir = -Util.Common.AngleToVector(Util.Common.VectorToAngle(dir));
                }
                else
                {
                    dir = ((Vector2)Camera.main.WorldToViewportPoint(transform.position));
                    dir.Scale(new Vector2(Screen.width, Screen.height));
                    dir = position - dir;
                    dir = -Util.Common.AngleToVector(Util.Common.VectorToAngle(dir));
                }
               
                rigidbody2D.AddForce(dir * 60);
                Debug.Log((Vector2)Camera.main.WorldToViewportPoint(transform.position));
            }
        }
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(new Vector2(0, 0), new Vector2(120, 30)), "Switch to Local Mode"))
            useLocalRelativePosition = !useLocalRelativePosition;
    }
}
