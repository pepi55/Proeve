using UnityEngine;
using System.Collections;

public class TargetControler : MonoBehaviour
{

    //bounderies the target moves in
    [SerializeField]
    Transform LeftWall, RightWall;
    Vector3 EndLeft, EndRight;

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
    int scoreCurvMax;
  
    void Start()
    {
        EndLeft = LeftWall.position;
        EndLeft.y = transform.position.y;
        EndLeft.z = transform.position.z;

        EndRight = RightWall.position;
        EndRight.y = transform.position.y;
        EndRight.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //EndLeft = LeftWall.position;
        //EndLeft.y = transform.position.y;
        //EndLeft.z = transform.position.z;

        //EndRight = RightWall.position;
        //EndRight.y = transform.position.y;
        //EndRight.z = transform.position.z;

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

        if (GameManager.Score > 0)
            speed = 0.5f + (-1 * Mathf.Exp(-GameManager.Score / (float)scoreCurvMax) + 1) * MaxSpeed;
        else
            speed = 0.5f;
        //1 - 2·exp(-x / 400)
    }
}
