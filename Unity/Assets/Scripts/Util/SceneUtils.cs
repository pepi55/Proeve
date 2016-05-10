using UnityEngine;
using System.Collections;

namespace Util
{
    /// <summary>
    /// Used in quick prototyping of buttons for the UI sytem
    /// </summary>
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