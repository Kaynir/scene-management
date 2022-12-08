using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.Scenes.SplashScreens
{
    public class ImageSplashScreen : SplashScreen
    {
        [SerializeField] private Image _splashImage = null;

        protected override void SetAlpha(float alpha)
        {
            _splashImage.canvasRenderer.SetAlpha(alpha);
            _splashImage.enabled = alpha > MIN_ALPHA;
        }
    }
}