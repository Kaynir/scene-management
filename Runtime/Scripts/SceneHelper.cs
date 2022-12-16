using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaynir.Scenes
{
    public static class SceneHelper
    {
        public static AsyncOperation LoadSingle(int sceneIndex)
        {
            return GetLoadOperation(sceneIndex, LoadSceneMode.Single, true);
        }

        public static AsyncOperation LoadAdditive(int sceneIndex, bool isActive = false)
        {
            return GetLoadOperation(sceneIndex, LoadSceneMode.Additive, isActive);
        }

        public static AsyncOperation Unload(int sceneIndex)
        {
            return SceneManager.UnloadSceneAsync(sceneIndex);
        }

        public static void SetActive(int sceneIndex)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
            SceneManager.SetActiveScene(scene);
        }

        private static AsyncOperation GetLoadOperation(int sceneIndex, LoadSceneMode mode, bool isActive)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, mode);

            if (isActive)
            {
                operation.completed += (op) =>
                {
                    SetActive(sceneIndex);
                };
            }

            return operation;
        }
    }
}