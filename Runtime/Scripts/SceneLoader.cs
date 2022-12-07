using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaynir.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SplashScreen _splashScreen = null;
        [SerializeField] private List<int> _persistentScenes = new List<int>() { 0 };

        public bool IsLoading { get; private set; }

        public void Load(int mainScene, List<int> extraScenes)
        {
            if (IsLoading) return;
            StartCoroutine(LoadRoutine(mainScene, extraScenes));
        }

        public void Load(int mainScene) => Load(mainScene, new List<int>());

        public AsyncOperation LoadAdditive(int sceneIndex, bool isActive)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            if (isActive)
            {
                operation.completed += (op) =>
                {
                    Scene scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
                    SceneManager.SetActiveScene(scene);
                };
            }

            return operation;
        }

        public AsyncOperation Unload(int sceneIndex)
        {
            return SceneManager.UnloadSceneAsync(sceneIndex);
        }

        private IEnumerator LoadRoutine(int mainScene, List<int> extraScenes)
        {
            IsLoading = true;

            yield return _splashScreen.FadeInRoutine();

            yield return UnloadCurrentScenesRoutine();
            yield return LoadAdditive(mainScene, true);

            foreach (int index in extraScenes)
            {
                yield return LoadAdditive(index, false);
            }

            yield return _splashScreen.FadeOutRoutine();

            IsLoading = false;
        }

        private IEnumerator UnloadCurrentScenesRoutine()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                
                if (!_persistentScenes.Contains(scene.buildIndex))
                {
                    yield return Unload(scene.buildIndex);
                }
            }
        }
    }
}