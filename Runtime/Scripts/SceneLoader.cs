using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaynir.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SplashScreen _splashScreen = null;

        public bool IsLoading { get; private set; }

        public void FadeInScene(int targetScene, List<int> additiveScenes)
        {
            if (IsLoading) return;
            StartCoroutine(LoadRoutine(targetScene, additiveScenes));
        }

        public void FadeInScene(int targetScene)
        {
            FadeInScene(targetScene, new List<int>());
        }

        public AsyncOperation LoadScene(int sceneIndex, LoadSceneMode mode)
        {
            return SceneManager.LoadSceneAsync(sceneIndex, mode);
        }

        public void UnloadScene(int sceneIndex)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }

        private IEnumerator LoadRoutine(int targetScene, List<int> additiveScenes)
        {
            IsLoading = true;

            yield return _splashScreen.FadeInRoutine();
            
            yield return LoadScene(targetScene, LoadSceneMode.Single);

            foreach (int scene in additiveScenes)
            {
                yield return LoadScene(scene, LoadSceneMode.Additive);
            }

            yield return _splashScreen.FadeOutRoutine();

            IsLoading = false;
        }
    }
}