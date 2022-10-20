using UnityEngine;

namespace Kaynir.Scenes
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private ProgressHandler[] _progressHandlers = null;

        public void SetActive(bool isActive)
        {
            _canvas.enabled = isActive;
        }

        public void SetProgress(float progress)
        {
            for (int i = 0; i < _progressHandlers.Length; i++)
            {
                _progressHandlers[i].SetProgress(progress);
            }
        }
    }
}