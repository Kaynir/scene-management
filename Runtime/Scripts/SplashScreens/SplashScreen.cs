using System.Collections;
using UnityEngine;

namespace Kaynir.Scenes.SplashScreens
{
    public abstract class SplashScreen : MonoBehaviour
    {
        public const float MIN_ALPHA = 0f;
        public const float MAX_ALPHA = 1f;

        [SerializeField] private SplashState _startState = SplashState.None;
        [SerializeField] private BlendSettings _fadeInConfig = null;
        [SerializeField] private BlendSettings _fadeOutConfig = null;

        protected virtual void Start()
        {
            SetState(_startState);
        }

        [ContextMenu("Fade In")]
        public void FadeIn()
        {
            StopAllCoroutines();
            StartCoroutine(FadeInRoutine());
        }

        [ContextMenu("Fade Out")]
        public void FadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutRoutine());
        }

        public IEnumerator FadeInRoutine()
        {
            yield return FadeRoutine(_fadeInConfig, MAX_ALPHA);
        }

        public IEnumerator FadeOutRoutine()
        {
            yield return FadeRoutine(_fadeOutConfig, MIN_ALPHA);
        }

        public void SetState(SplashState state)
        {
            switch (state)
            {
                default: break;
                case SplashState.MinAlpha: SetAlpha(MIN_ALPHA); break;
                case SplashState.MaxAlpha: SetAlpha(MAX_ALPHA); break;
                case SplashState.FadeIn: FadeIn(); break;
                case SplashState.FadeOut: FadeOut(); break;
            }
        }

        protected abstract void SetAlpha(float alpha);

        private IEnumerator FadeRoutine(BlendSettings blend, float alpha)
        {
            for (float t = 0; t < blend.Time; t += Time.unscaledDeltaTime)
            {
                SetAlpha(blend.Evaluate(t));
                yield return null;
            }

            SetAlpha(alpha);
        }
    }
}