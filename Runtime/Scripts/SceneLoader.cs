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
            StartCoroutine(LoadRoutine(activeScene, extraScenes));
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

        public AsyncOperation LoadScene(int sceneIndex, LoadSceneMode mode)
        {
            return SceneManager.LoadSceneAsync(sceneIndex, mode);
        }

        public void UnloadScene(int sceneIndex)
        {
            if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded) return;
            SceneManager.UnloadSceneAsync(sceneIndex);
        }

        private IEnumerator LoadRoutine(int activeScene, List<int> extraScenes)
        {
            IsLoading = true;

            yield return _splashScreen.FadeInRoutine();
            
            yield return LoadScene(activeScene, LoadSceneMode.Single);

            foreach (int scene in extraScenes)
            {
                yield return LoadScene(scene, LoadSceneMode.Additive);
            }

            yield return _splashScreen.FadeOutRoutine();

            IsLoading = false;
        }
    }
}