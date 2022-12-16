using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.Scenes.SplashScreens
{
    public class ImageFadeScreen : FadeScreen
    {
        [SerializeField] private Image _fadeImage = null;

        public override float GetAlpha()
        {
            return _fadeImage.color.a;
        }

        protected override void SetAlpha(float alpha)
        {
            Color color = _fadeImage.color;
            color.a = alpha;

            _fadeImage.color = color;
            _fadeImage.enabled = alpha > MIN_ALPHA;
        }
    }
}