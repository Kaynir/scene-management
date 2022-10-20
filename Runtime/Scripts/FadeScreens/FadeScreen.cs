using System.Collections;
using UnityEngine;

namespace Kaynir.Scenes
{
    public abstract class FadeScreen : MonoBehaviour
    {
        public const float MIN_ALPHA = 0f;
        public const float MAX_ALPHA = 1f;

        [Header("Fade In Settings:")]
        [SerializeField, Min(0f)] protected float _inTime = 0.5f;
        [SerializeField] protected AnimationCurve _inCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        
        [Header("Fade Out Settings:")]
        [SerializeField, Min(0f)] protected float _outTime = 0.5f;
        [SerializeField] protected AnimationCurve _outCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

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
            yield return FadeRoutine(_inTime, _inCurve, MAX_ALPHA);
        }

        public IEnumerator FadeOutRoutine()
        {
            yield return FadeRoutine(_outTime, _outCurve, MIN_ALPHA);
        }

        protected abstract void SetAlpha(float alpha);

        private IEnumerator FadeRoutine(float time, AnimationCurve curve, float alpha)
        {
            for (float t = 0; t < time; t+= Time.unscaledDeltaTime)
            {
                SetAlpha(curve.Evaluate(t / time));
                yield return null;
            }

            SetAlpha(alpha);
        }
    }
}