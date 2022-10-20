using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaynir.Scenes
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        public const float MAX_PROGRESS = 0.9f;

        [SerializeField] private FadeScreen _fadeScreen = null;
        [SerializeField] private LoadingScreen _loadingScreen = null;

        public bool IsLoading { get; private set; }

        public void Load(int sceneIndex)
        {
            if (IsLoading) return;
            StartCoroutine(LoadRoutine(sceneIndex));
        }

        public void LoadAdditive(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        public void Unload(int sceneIndex)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }

        private IEnumerator LoadRoutine(int sceneIndex)
        {
            IsLoading = true;

            if (_fadeScreen)
            {
                yield return _fadeScreen.FadeInRoutine();
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

            yield return _loadingScreen ? LoadingScreenRoutine(asyncLoad) : asyncLoad;

            if (_fadeScreen)
            {
                yield return _fadeScreen.FadeOutRoutine();
            }
            
            IsLoading = false;
        }

        private IEnumerator LoadingScreenRoutine(AsyncOperation asyncLoad)
        {
            _loadingScreen.SetActive(true);

            while (!asyncLoad.isDone)
            {
                _loadingScreen.SetProgress(asyncLoad.progress / MAX_PROGRESS);
                yield return null;
            }

            _loadingScreen.SetActive(false);
        }
    }
}