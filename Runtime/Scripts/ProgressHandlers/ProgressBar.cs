using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.Scenes
{
    public class ProgressBar : ProgressHandler
    {
        [SerializeField] private Image _filledImage = null;

        public override void SetProgress(float progress)
        {
            _filledImage.fillAmount = progress;
        }
    }
}