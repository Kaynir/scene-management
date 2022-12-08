using System.Collections;
using System.Collections.Generic;
using Kaynir.Scenes.SplashScreens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaynir.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SplashScreen _splashScreen = null;

        public bool IsLoading { get; private set; }

        public void LoadSceneFaded(int activeScene, List<int> extraScenes)
        {
            if (IsLoading) return;
            StartCoroutine(LoadFadedRoutine(activeScene, extraScenes));
        }

        public void LoadSceneFaded(int activeScene)
        {
            LoadSceneFaded(activeScene, new List<int>());
        }

        public void LoadSceneFaded(int activeScene, params int[] extraScenes)
        {
            LoadSceneFaded(activeScene, new List<int>(extraScenes));
        }

        public void LoadSceneFaded(SceneCollection collection)
        {
            LoadSceneFaded(collection.ActiveScene, collection.ExtraScenes);
        }

        public void LoadScene(int sceneIndex, LoadSceneMode mode)
        {
            GetLoadSceneOperation(sceneIndex, mode);
        }

        public void UnloadScene(int sceneIndex)
        {
            if (!IsLoadedScene(sceneIndex))
            {
                Debug.LogWarning($"Unload operation failed. Scene [{sceneIndex}] is not loaded.");
                return;
            }

            GetUnloadSceneOperation(sceneIndex);
        }

        public bool IsLoadedScene(int sceneIndex)
        {
            return SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded;
        }

        private AsyncOperation GetLoadSceneOperation(int sceneIndex, LoadSceneMode mode)
        {
            return SceneManager.LoadSceneAsync(sceneIndex, mode);
        }

        private AsyncOperation GetUnloadSceneOperation(int sceneIndex)
        {
            return SceneManager.UnloadSceneAsync(sceneIndex);
        }

        private IEnumerator LoadFadedRoutine(int activeScene, List<int> extraScenes)
        {
            IsLoading = true;
            yield return _splashScreen.FadeInRoutine();
            
            yield return GetLoadSceneOperation(activeScene, LoadSceneMode.Single);

            foreach (int scene in extraScenes)
            {
                yield return GetLoadSceneOperation(scene, LoadSceneMode.Additive);
            }

            yield return _splashScreen.FadeOutRoutine();
            IsLoading = false;
        }
    }
}