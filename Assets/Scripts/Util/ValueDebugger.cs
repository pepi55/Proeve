using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace Util
{
    public class ValueDebugger : MonoBehaviour
    {
        static ValueDebugger instance;

        protected Dictionary<string, object> Values;
        protected Text t;

        public static void ValueLog(string name, object value)
        {
            if (instance == null)
            {

                //Canvas 
                GameObject g = new GameObject();

                Canvas c = g.AddComponent<Canvas>();
                c.renderMode = RenderMode.ScreenSpaceOverlay;
                c.sortingOrder = 7000;

                CanvasScaler sc = g.AddComponent<CanvasScaler>();
                sc.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                sc.referenceResolution = new Vector2(1600, 900);

                //text Display
                GameObject g2 = new GameObject();
                g2.transform.SetParent(g.transform, false);

                RectTransform rt = g2.AddComponent<RectTransform>();
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0.5f, 0);
                rt.sizeDelta = new Vector2(-20, -40);

                rt.anchoredPosition = new Vector2(-20, 0);

                Text t = g2.AddComponent<Text>();
                t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                t.color = Color.green;
                t.fontSize = 20;
                g2.AddComponent<ValueDebugger>();

                //background Image
                GameObject g3 = new GameObject();
                g3.transform.SetParent(g.transform, false);
                g3.transform.SetAsFirstSibling();

                rt = g3.AddComponent<RectTransform>();
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0.5f, 0);
                rt.sizeDelta = new Vector2(-20, -40);

                rt.anchoredPosition = new Vector2(-20, 0);
                

                Image I = g3.AddComponent<Image>();
                I.color = new Color(0.1f, 0.1f, 0.1f, 0.7f);


                g.name = "util.DebugVisual";
            }

            if (instance && !instance.transform.parent.gameObject.activeSelf) 
                instance.transform.parent.gameObject.SetActive(true);

            if (instance.Values.Keys.Contains(name))
            {
                instance.Values[name] = value;
            }
            else
            {
                instance.Values.Add(name, value);
            }
        }

        void Awake()
        {
            instance = this;
            t = GetComponent<Text>();
            Values = new Dictionary<string, object>();
        }

        void Update()
        {
            if (!Debugger.DebugEnabled)
                transform.parent.gameObject.SetActive(false);

            Process();
        }

        void OnDestroy()
        {
            instance = null;
        }

        string ts;
        void Process()
        {
            ts = "";

            foreach (KeyValuePair<string, object> vs in Values)
            {
                ts += vs.Key + " : " + vs.Value.ToString() + " \n";
            }

            t.text = ts;
        }
    }

    public static class Debugger
    {
        public static bool DebugEnabled = true;

        public static void Log(string name, object value)
        {
            if (DebugEnabled)

                ValueDebugger.ValueLog(name, value);
        }
    }
}
