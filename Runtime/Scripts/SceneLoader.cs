using System;
using System.Collections;
using System.Collections.Generic;
using Kaynir.Scenes.SplashScreens;
using UnityEngine;

namespace Kaynir.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        public static event Action OnLoadCompleted;

        [field: SerializeField] public SceneReference LoadingScene { get; private set; } = new SceneReference();
        [field: SerializeField] public FadeScreen FadeScreen { get; private set; } = null;

        public bool IsLoading { get; private set; }

        public void Load(List<SceneReference> sceneList)
        {
            if (IsLoading) return;
            IsLoading = true;
            
            StartCoroutine(FadeScreen
            ? LoadFadedRoutine(sceneList)
            : LoadRoutine(sceneList));
        }

        private IEnumerator LoadRoutine(List<SceneReference> sceneList)
        {
            if (!LoadingScene.IsLoaded)
            {
                yield return SceneHelper.LoadSingle(LoadingScene);
            }

            for (int i = 0; i < sceneList.Count; i++)
            {
                yield return SceneHelper.LoadAdditive(sceneList[i], i == 0);
            }

            yield return SceneHelper.Unload(LoadingScene);

            IsLoading = false;
            OnLoadCompleted?.Invoke();
        }

        private IEnumerator LoadFadedRoutine(List<SceneReference> sceneList)
        {
            yield return FadeScreen.FadeInRoutine();
            yield return LoadRoutine(sceneList);
            yield return FadeScreen.FadeOutRoutine();
        }
    }
}