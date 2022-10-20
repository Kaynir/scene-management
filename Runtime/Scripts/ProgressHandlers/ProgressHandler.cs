using UnityEngine;

namespace Kaynir.Scenes
{
    public abstract class ProgressHandler : MonoBehaviour
    {
        public abstract void SetProgress(float progress);
    }
}