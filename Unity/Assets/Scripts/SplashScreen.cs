using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [Tooltip("A black overlay image"),SerializeField]
    UnityEngine.UI.Image Overlay;

    void Start()
    {
        StartCoroutine(Sequence());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Util.SceneControler.Load("MainMenu");
        }
    }

    IEnumerator Sequence()
    {
        float time;
        float duration;
        Color c = Overlay.color;
        time = 0.5f;
        duration = 1;


        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        time = 1f;

        while (time > 0)
        {
            Overlay.color = new Color(c.r, c.g, c.b, (time / duration));
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        time = 2;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        time = 0;

        while (time < duration)
        {
            Overlay.color = new Color(c.r, c.g, c.b, (time / duration));
            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Overlay.color = c;

        time = 2;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Util.SceneControler.Load("MainMenu");
    }
}
