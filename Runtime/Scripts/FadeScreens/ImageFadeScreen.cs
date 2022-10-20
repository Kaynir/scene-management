using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.Scenes
{
    public class ImageFadeScreen : FadeScreen
    {
        [Header("Image Settings:")]
        [SerializeField] private Image _image = null;

        protected override void SetAlpha(float alpha)
        {
            _image.canvasRenderer.SetAlpha(alpha);
            _image.enabled = alpha > MIN_ALPHA;
        }
    }
}