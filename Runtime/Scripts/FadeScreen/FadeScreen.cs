using System.Collections;
using UnityEngine;

namespace Kaynir.Scenes.SplashScreens
{
    public abstract class FadeScreen : MonoBehaviour
    {
        public const float MIN_ALPHA = 0f;
        public const float MAX_ALPHA = 1f;

        [SerializeField] private FadeState _initialState = FadeState.None;
        [SerializeField] private float _fadeTime = .5f;
        [SerializeField] private AnimationCurve _fadeInCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _fadeOutCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

        protected virtual void Start()
        {
            SetState(_initialState);
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
            yield return FadeRoutine(_fadeInCurve, MAX_ALPHA);
        }

        public IEnumerator FadeOutRoutine()
        {
            yield return FadeRoutine(_fadeOutCurve, MIN_ALPHA);
        }

        public void SetState(FadeState state)
        {
            switch (state)
            {
                default: break;
                case FadeState.MinAlpha: SetAlpha(MIN_ALPHA); break;
                case FadeState.MaxAlpha: SetAlpha(MAX_ALPHA); break;
                case FadeState.FadeIn: FadeIn(); break;
                case FadeState.FadeOut: FadeOut(); break;
            }
        }

        public abstract float GetAlpha();

        protected abstract void SetAlpha(float alpha);

        private IEnumerator FadeRoutine(AnimationCurve curve, float endAlpha)
        {
            float startAlpha = GetAlpha();

            if (startAlpha == endAlpha) yield break;

            for (float t = 0; t < _fadeTime; t += Time.unscaledDeltaTime)
            {
                SetAlpha(curve.Evaluate(t / _fadeTime));
                yield return null;
            }

            SetAlpha(endAlpha);
        }
    }
}