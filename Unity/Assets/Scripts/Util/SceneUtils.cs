using UnityEngine;
using System.Collections;

namespace Util
{
    public class SceneUtils : MonoBehaviour
    {

        public void OpenScene(string name)
        {
            SceneControler.Load(name);
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}