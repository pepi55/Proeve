using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Util
{
    public class SliderSetter : MonoBehaviour
    {
        public Slider slider;
        public ScrollRect scrollRect;
        public float StartValue;

        void Start()
        {
            slider.value = StartValue;
            scrollRect.verticalNormalizedPosition = StartValue;
        }
    }
}