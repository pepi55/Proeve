using UnityEngine;
using System.Collections;

/// <summary>
/// Snaps a Object to a position on screen
/// </summary>
public class SnapToScreenPoint : MonoBehaviour
{

    [SerializeField]
    new Camera camera;

    /// <summary>
    /// Position on screen
    /// Value should be between -100 and 100
    /// </summary>
    [Tooltip("Should be value between -100 and 100")]
    public Vector3 screenPosition = Vector3.zero;

    /// <summary>
    /// Sprite size in pixels to make sure it's does not get streched 
    /// </summary>
    [Tooltip("Sprite's Size in pixel used to make sure it's scaled correctly")]
    public Vector2 StartSize = Vector2.one;

    /// <summary>
    /// Center of the sprite so one can set a custom pivot
    /// </summary>
    [Tooltip("this position will be seen as the center of the object. Should be between -1 and 1")]
    public Vector2 CenterPosition = Vector2.one / 2f;

    /// <summary>
    /// Should the object be moved in this direction
    /// </summary>
    [SerializeField, Tooltip("Should Object be moved in this direction?")]
    bool Vertical = false, Horizontal = false;
    [SerializeField, Tooltip("use center point")]
    bool UsePivot = false;

    void Start()
    {
        if (camera == null)
            camera = Camera.main;

        screenPosition /= 100f;
        StartSize /= 100f;

        DoMove();

        //if (GetComponent<SpriteRenderer>())
        //{
        //    SpriteRenderer spr = GetComponent<SpriteRenderer>();
        //    StartSize = spr.bounds.size;
        //    StartSize.x += transform.localScale.x;
        //    StartSize.y += transform.localScale.y;
        //}
    }

    void Update()
    {

        DoMove();
    }

    void DoMove()
    {
        Vector3 p1 = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        p1.z = transform.position.z;
        p1.y *= screenPosition.y;
        p1.x *= screenPosition.x;

        Debug.Log(p1 + gameObject.name);

        // p1 *= 2f;
        if (!Vertical)
            p1.y = transform.position.y;

        if (!Horizontal)
            p1.x = transform.position.x;

        if (UsePivot)
        {
            if (Vertical)
                if (screenPosition.y > 0)
                    p1.y -= (transform.localScale.y / 2f) * StartSize.y;
                else
                    p1.y += (transform.localScale.y / 2f) * StartSize.y;

            if (Horizontal)
                if (screenPosition.x > 0)
                    p1.x -= (transform.localScale.x / 2f) * StartSize.x;
                else
                    p1.x += (transform.localScale.x / 2f) * StartSize.x;

        }

        transform.position = p1;
    }
}