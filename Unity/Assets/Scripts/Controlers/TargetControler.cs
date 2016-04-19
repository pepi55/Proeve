using UnityEngine;
using System.Collections;

public class TargetControler : MonoBehaviour
{

    //bounderies the target moves in
    [SerializeField]
    Transform LeftWall = null, RightWall = null;
    Vector3 EndLeft = Vector3.zero, EndRight = Vector3.zero;

    /// <summary>
    /// Move direction 1 = right, left = -1, none = 0;
    /// </summary>
    int dir = 1;

    /// <summary>
    /// Speed the ball moves it
    /// </summary>
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    float MaxSpeed = 4f;
    [SerializeField]
    int scoreCurvMax = 0;

    /// <summary>
    /// When a active the target does not move
    /// </summary>
    bool frozen = false;


    void Start()
    {
        EndLeft = LeftWall.position;
        EndLeft.y = transform.position.y;
        EndLeft.z = transform.position.z;

        EndRight = RightWall.position;
        EndRight.y = transform.position.y;
        EndRight.z = transform.position.z;

        Events.GlobalEvents.AddEventListener<Events.IPause>(OnPause);
        Events.GlobalEvents.AddEventListener<Events.IResetGameState>(OnBallReset);

        InputManager.onClick += InputManager_onClick;
    }

    public void OnDestroy()
    {
        InputManager.onClick -= InputManager_onClick;

        Events.GlobalEvents.RemoveEventListener<Events.IPause>(OnPause);
        Events.GlobalEvents.RemoveEventListener<Events.IResetGameState>(OnBallReset);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.GamePaused)
        {
            return;
        }

        if (dir > 0)
        {
            if (Vector3.Distance(EndRight, transform.position) < 1f)
            {
                dir = -1;
            }
        }
        else if (dir < 0) 
        {
            if (Vector3.Distance(EndLeft, transform.position) < 1f)
            {
                dir = 1;
            }
        }

        if(dir!=0)
        {
            transform.Translate(Vector3.right * dir * Time.deltaTime * speed);
        }

        if (GameManager.Score > 0 && scoreCurvMax != 0)
            speed = 0.5f + (-1 * Mathf.Exp(-GameManager.Score / (float)scoreCurvMax) + 1) * MaxSpeed;
        else
            speed = 0.5f;
        //1 - 2·exp(-x / 400)
    }

    void OnPause(Events.IPause obj)
    {
        enabled = !obj.State;
    }

    private void InputManager_onClick(Vector2 position)
    {
        if(enabled)
        {
            Debug.Log("toggled Frozen");
            frozen = false;
        }
    }

    void OnBallReset(Events.IResetGameState obj)
    {
        frozen = true;
    }
}
