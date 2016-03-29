//Author Jesse Stam
//Created 12-2-2016
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Util
{
    public static class SceneControler
    {
        public static void Load(string SceneName)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        }

        public static void LoadAddative(string SceneName)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

        public static void UnloadScene(string SceneName)
        {
            int scenecount = SceneManager.sceneCount;

            for (int i = 0; i < scenecount; i++)
            {
                if (SceneName == SceneManager.GetSceneAt(i).name)
                    if (SceneManager.GetSceneAt(i).isLoaded)
                        SceneManager.UnloadScene(i);
            }
        }
    }
}
