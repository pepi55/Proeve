using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallControler : MonoBehaviour
{

    new Rigidbody2D rigidbody2D;

    bool useLocalRelativePosition;
    bool UseXAxis;
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
                    dir = Util.Common.AngleToVector(Util.Common.VectorToAngle(dir));
                }
                else
                {
                    dir = ((Vector2)Camera.main.WorldToViewportPoint(transform.position));
                    dir.Scale(new Vector2(Screen.width, Screen.height));
                    dir = position - dir;

                    dir = Util.Common.AngleToVector(Util.Common.VectorToAngle(dir));
                }
                //dir = new Vector2(-dir.x, dir.y * Physics2D.gravity.y);
                dir = -dir * 200 * rigidbody2D.mass;
                dir.y *= rigidbody2D.gravityScale;
                rigidbody2D.AddForce(dir);

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "ScoreTarget")
        {
            Events.GlobalEvents.Invoke(new Events.IScore());
            transform.position = Vector3.zero;
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(new Vector2(0, 0), new Vector2(230, 30)), useLocalRelativePosition ? "Switch to static center point mode" : "Switch to relative to ball mode"))
        {
            useLocalRelativePosition = !useLocalRelativePosition;
        }

        if (GUI.Button(new Rect(new Vector2(0, 35), new Vector2(230, 30)), UseXAxis ? "Don't Use X Axis" : "Use X Axis"))
        {
            UseXAxis = !UseXAxis;

            if (UseXAxis)
            {
                rigidbody2D.constraints = RigidbodyConstraints2D.None;
            }
            else
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }

    }
}
