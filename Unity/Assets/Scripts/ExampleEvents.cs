using UnityEngine;
using System.Collections;

public class Benchmark : MonoBehaviour {

    int j = 0;

    delegate void Delegate();
    event Delegate onStuff;

	// Use this for initialization
	void Start () {
        System.DateTime start = System.DateTime.Now;

        Events.GlobalEvents.AddEventListener<A>(EventT);

        for (int i = 0; i < 1000; i++)
        {
            Events.GlobalEvents.Invoke(new A());
        }
        Debug.Log((System.DateTime.Now - start).TotalMilliseconds + "ms");
        j = 0;
        start = System.DateTime.Now;

        onStuff += Test;

        for (int i = 0; i < 1000; i++)
        {
            if (onStuff != null)
                onStuff();
        }
        Debug.Log((System.DateTime.Now - start).TotalMilliseconds + "ms");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void EventT(A ev)
    {
        j++;
        Debug.LogWarning(j);
    }

    void Test()
    {
        j++;
        Debug.LogWarning(j);
    }
}

public class A:Events.IEvent
{

}
