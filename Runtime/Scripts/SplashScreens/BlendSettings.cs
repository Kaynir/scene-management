using UnityEngine;

namespace Kaynir.Scenes.SplashScreens
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Scenes/Blend Settings")]
    public class BlendSettings : ScriptableObject
    {
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField, Min(0)] private float _time = 1;

        public AnimationCurve Curve => _curve;
        public float Time => _time;

        public float Evaluate(float elapsedTime)
        {
            return _curve.Evaluate(elapsedTime / _time);
        }
    }
}