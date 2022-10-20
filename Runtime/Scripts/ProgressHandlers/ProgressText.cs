using UnityEngine;
using TMPro;

namespace Kaynir.Scenes
{
    public class ProgressText : ProgressHandler
    {
        private const int PERCENTAGE_MODIFIER = 100;

        [SerializeField] private TMP_Text _textField = null;
        [SerializeField] private string _textFormat = "{0:00}%";

        public override void SetProgress(float progress)
        {
            _textField.SetText(_textFormat, progress * PERCENTAGE_MODIFIER);
        }
    }
}