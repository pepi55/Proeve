using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallControler : MonoBehaviour
{

    new Rigidbody2D rigidbody2D;

    //tempvalue for when game is paused
    Vector2 savedSpeed;

    bool useLocalRelativePosition = true;
    bool UseXAxis = true;

    /// <summary>
    /// ball is frozen into place
    /// </summary>
    bool frozen; //TODO Change frozen to is kinametic from ridgidbody2d

    // Use this for initialization
    void Start()
    {
        InputManager.onClick += InputManager_onClick;

        Events.GlobalEvents.AddEventListener<Events.IPause>(OnPause);
        Events.GlobalEvents.AddEventListener<Events.IResetGameState>(ResetBall);
        rigidbody2D = GetComponent<Rigidbody2D>();

        ResetBall(new Events.IResetGameState());

        ChangeLooks();
    }

    public void ChangeLooks()
    {
        ShopMenuData data;

        data = FindObjectOfType<ShopMenuData>();
        if(!data)
        {
            GameObject gameobj = Instantiate(Resources.Load(ShopMenuData.ResourceName)) as GameObject;

            data = gameobj.GetComponent<ShopMenuData>();
        }

        GetComponent<SpriteRenderer>().sprite = data.Characters[SaveManager.savaData.SelectedCharacter].LowRes;
    }

    public void OnDestroy()
    {
        InputManager.onClick -= InputManager_onClick;

        Events.GlobalEvents.RemoveEventListener<Events.IPause>(OnPause);
        Events.GlobalEvents.RemoveEventListener<Events.IResetGameState>(ResetBall);
    }

    /// <summary>
    /// Called when pause state has changed
    /// </summary>
    /// <param name="pause"></param>
    void OnPause(Events.IPause pause)
    {
        enabled = !pause.State;
        if (pause.State)
        {
            savedSpeed = rigidbody2D.velocity;
            rigidbody2D.velocity = Vector2.zero;
            frozen = true;
            SetConstraints();
        }
        else
        {
            frozen = false;
            SetConstraints();
            rigidbody2D.velocity = savedSpeed;

        }
    }

    void ResetBall(Events.IResetGameState obj)
    {
        transform.position = Vector3.zero;
        rigidbody2D.velocity = Vector2.zero;
        frozen = true;
        SetConstraints();
        //StartCoroutine(ballFreeze(0.5f, 0.0f));
    }

/// <summary>
/// Call in inputManager handles force adding for the ball
/// </summary>
/// <param name="position"></param>
    private void InputManager_onClick(Vector2 position)
    {
        if (isActiveAndEnabled)
        {
            frozen = false;
            SetConstraints();

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
                dir = -dir * 300 * rigidbody2D.mass; //TODO tweak with force to make the ball more reactive
                dir.y *= rigidbody2D.gravityScale;

                rigidbody2D.AddTorque(dir.x/10);
                if (rigidbody2D.angularVelocity > 10)
                    rigidbody2D.angularVelocity = 10;
                if (rigidbody2D.angularVelocity < -10)
                    rigidbody2D.angularVelocity = -10;
                rigidbody2D.AddForce(dir);

            }
        }
    }

    /// <summary>
    /// Unity Function
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "ScoreTarget")
        {
            Events.GlobalEvents.Invoke(new Events.IScore());
            StartCoroutine(ballResetDelay(0.2f));
            StartCoroutine(ballFreeze(0.5f, 0.2f));
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bottom")
        {           
            rigidbody2D.velocity = Vector2.zero;
           // StartCoroutine(ballResetDelay(0.2f));
           // StartCoroutine(ballFreeze(0.5f, 0.2f));
            Events.GlobalEvents.Invoke(new Events.IPlayerHitBottom());
        }
    }

    /// <summary>
    /// Used to induce a delay for when the ball resets
    /// </summary>
    /// <param name="Delay">Delay Time in seconds</param>
    /// <returns></returns>
    IEnumerator ballResetDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        transform.position = Vector3.zero;

    }

    /// <summary>
    /// Freezes the Ball in place for x seconds
    /// </summary>
    /// <param name="duration">time ball will befrozen</param>
    /// <param name="delay">a delay for this action to happen</param>
    /// <returns></returns>
    IEnumerator ballFreeze(float duration,float delay)
    {
        yield return new WaitForSeconds(delay);
        float d = duration;

        frozen = true;
        SetConstraints();

        while(d>0 && frozen)
        {
            d -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        frozen = false;
        SetConstraints();
        
    }

    /// <summary>
    /// Used to change the contrians of the object easily
    /// </summary>
    void SetConstraints()
    {
        if (frozen)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

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

    /// <summary>
    /// Used for Debuging and Trying out diffrent styles of ball movement and controle
    /// </summary>
    public void OnGUI()
    {
        if (GUI.Button(new Rect(new Vector2(0, 0), new Vector2(230, 30)), useLocalRelativePosition ? "Switch to static center point mode" : "Switch to relative to ball mode"))
        {
            useLocalRelativePosition = !useLocalRelativePosition;
        }

        if (GUI.Button(new Rect(new Vector2(0, 35), new Vector2(230, 30)), UseXAxis ? "Don't Use X Axis" : "Use X Axis"))
        {
            UseXAxis = !UseXAxis;

            SetConstraints();
        }

    }
}
